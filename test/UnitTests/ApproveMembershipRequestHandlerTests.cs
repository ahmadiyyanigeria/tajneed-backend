using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Commands;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;
using TajneedApi.Domain.ValueObjects;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Enums;

public class ApproveMembershipRequestHandlerTests
{
    private readonly Mock<IMembershipRequestRepository> _mockMemberRequestRepo;
    private readonly Mock<IMemberRepository> _mockMemberRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ICurrentUser> _mockCurrentUser;
    private readonly Mock<IOptions<ApprovalSettingsConfiguration>> _mockApprovalSettings;
    private readonly Mock<ILogger<ApproveMembershipRequest.Handler>> _mockLogger;

    public ApproveMembershipRequestHandlerTests()
    {
        _mockMemberRequestRepo = new Mock<IMembershipRequestRepository>();
        _mockMemberRepo = new Mock<IMemberRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCurrentUser = new Mock<ICurrentUser>();
        _mockApprovalSettings = new Mock<IOptions<ApprovalSettingsConfiguration>>();
        _mockLogger = new Mock<ILogger<ApproveMembershipRequest.Handler>>();
    }

    private ApproveMembershipRequest.Handler CreateHandler()
    {
        var approvalSettingsConfigurations = Options.Create(new ApprovalSettingsConfiguration{Roles =
        [
            new RoleSettings { RoleName = "CircuitPresident", Level = 0 },
            new RoleSettings { RoleName = "NationalOfficer", Level = 1 }
        ]});
        return new ApproveMembershipRequest.Handler(
            _mockMemberRequestRepo.Object,
            _mockMemberRepo.Object,
            _mockUnitOfWork.Object,
            _mockCurrentUser.Object,
            approvalSettingsConfigurations,            
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_UserWithoutApprovalPermission_ThrowsDomainException()
    {
        var handler = CreateHandler();
        var command = new ApproveMembershipRequest.ApproveMembershipRequestCommand
        {
            MembershipRequestIds = new List<string> { "req1", "req2" }
        };

        var currentUser = new UserDetails { Role = "Member", Email = "member@gmail.com" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);       

        var ex = await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None)
        );

        Assert.Equal(nameof(ExceptionCodes.AccessDeniedToApproveRequests), ex.ErrorCode);
    }

    [Fact]
    public async Task Handle_LastLevelApproval_ApprovesRequests()
    {
        var handler = CreateHandler();
        var command = new ApproveMembershipRequest.ApproveMembershipRequestCommand
        {
            MembershipRequestIds = new List<string> { "req1" }
        };

        var currentUser = new UserDetails { Role = "NationalOfficer", Email = "member@example.com", UserId = "member", Name = "Test member" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);

        var memberRequests = new List<MembershipRequest>
        {
            new MembershipRequest(
            surname: "Doe",
            firstName: "John",
            nationalityId: "US123456", 
            isBornMember: true, 
            auxiliaryBodyId: "Aux001", 
            middleName: "Edward", 
            dob: new DateTime(1990, 5, 15), 
            email: "john.doe@example.com", 
            phoneNo: "123-456-7890", 
            sex: Sex.Male, 
            maritalStatus: MaritalStatus.Single, 
            address: "123 Main St, Anytown, USA", 
            employmentStatus: EmploymentStatus.Employed, 
            occupation: "Software Developer", 
            batchRequestId: "Batch001", 
            jamaatId: "Jamaat001", 
            biatDate: new DateTime(2005, 8, 20)
        )
        };
        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockMemberRepo.Verify(m => m.CreateMemberAsync(It.IsAny<List<Member>>()), Times.Once);
    }

    [Fact]
    public async Task Handle_PartialApproval_PendingFinalApproval()
    {
        var handler = CreateHandler();
        var command = new ApproveMembershipRequest.ApproveMembershipRequestCommand
        {
            MembershipRequestIds = new List<string> { "req1" }
        };

        var currentUser = new UserDetails { Role = "CircuitPresident", Email = "approver@example.com", UserId = "user1", Name = "John Doe" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);
        
        var memberRequests = new List<MembershipRequest>
        {
            new MembershipRequest(
            surname: "Doe",
            firstName: "John",
            nationalityId: "US123456", 
            isBornMember: true, 
            auxiliaryBodyId: "Aux001", 
            middleName: "Edward", 
            dob: new DateTime(1990, 5, 15), 
            email: "john.doe@example.com", 
            phoneNo: "123-456-7890", 
            sex: Sex.Male, 
            maritalStatus: MaritalStatus.Single, 
            address: "123 Main St, Anytown, USA", 
            employmentStatus: EmploymentStatus.Employed, 
            occupation: "Software Developer", 
            batchRequestId: "Batch001", 
            jamaatId: "Jamaat001", 
            biatDate: new DateTime(2005, 8, 20)
        )
        };

        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockMemberRepo.Verify(m => m.CreateMemberAsync(It.IsAny<List<Member>>()), Times.Never);
    }
}
