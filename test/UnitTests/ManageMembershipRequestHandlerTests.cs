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
using static TajneedApi.Application.Commands.ManageMembershipRequest;

public class ManageMembershipRequestHandlerTests
{
    private readonly Mock<IMembershipRequestRepository> _mockMemberRequestRepo;
    private readonly Mock<IMemberRepository> _mockMemberRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ICurrentUser> _mockCurrentUser;
    private readonly Mock<IOptions<ApprovalSettingsConfiguration>> _mockApprovalSettings;
    private readonly Mock<ILogger<ManageMembershipRequest.Handler>> _mockLogger;

    public ManageMembershipRequestHandlerTests()
    {
        _mockMemberRequestRepo = new Mock<IMembershipRequestRepository>();
        _mockMemberRepo = new Mock<IMemberRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCurrentUser = new Mock<ICurrentUser>();
        _mockApprovalSettings = new Mock<IOptions<ApprovalSettingsConfiguration>>();
        _mockLogger = new Mock<ILogger<ManageMembershipRequest.Handler>>();
    }

    private Handler CreateHandler()
    {
        var approvalSettingsConfigurations = Options.Create(new ApprovalSettingsConfiguration
        {
            Roles =
        [
            new RoleSettings { RoleName = "CircuitPresident", Level = 0 },
            new RoleSettings { RoleName = "NationalOfficer", Level = 1 }
        ]
        });
        return new ManageMembershipRequest.Handler(
            _mockMemberRequestRepo.Object,
            _mockMemberRepo.Object,
            _mockUnitOfWork.Object,
            _mockCurrentUser.Object,
            approvalSettingsConfigurations,
            _mockLogger.Object
        );
    }
    [Fact]
    public async Task Handle_UserWithInvalidRole_ThrowsDomainException()
    {
        var handler = CreateHandler();
        var command = new ManageMembershipRequestCommand
        {
            MembershipRequests = [new MembershipRequestViewModel{MembershipRequestId = "req1"}]
        };

        var currentUser = new UserDetails { Role = "InvalidRole", Email = "invalid@example.com" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);

        var ex = await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None)
        );

        Assert.Equal(nameof(ExceptionCodes.AccessDeniedToApproveRequests), ex.ErrorCode);
    }

    [Fact]
    public async Task Handle_RejectionOfMembershipRequest_UpdatesRequestStatusToRejected()
    {
        var handler = CreateHandler();
        var currentUser = new UserDetails { Role = "NationalOfficer", Email = "officer@example.com", UserId = "officer", Name = "Test Officer" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);
        var memberRequest = new MembershipRequest(
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
        );
        var memberRequests = new List<MembershipRequest>
        {
            memberRequest
        };
        var command = new ManageMembershipRequestCommand
        {
            MembershipRequests = [new MembershipRequestViewModel{MembershipRequestId = memberRequest.Id, Action = ActionType.Reject}]
        };

        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(RequestStatus.Rejected, memberRequests[0].RequestStatus);
    }

    [Fact]
    public async Task Handle_UnsupportedActionType_ThrowsDomainException()
    {
        var handler = CreateHandler();
        var memberRequest = new MembershipRequest(
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
        );
        var memberRequests = new List<MembershipRequest>
        {
            memberRequest
        };
        var command = new ManageMembershipRequestCommand
        {
            MembershipRequests = [new MembershipRequestViewModel{MembershipRequestId = memberRequest.Id, Action = (ActionType)10}]
        };

        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);
        var currentUser = new UserDetails { Role = "NationalOfficer", Email = "officer@example.com" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);

        var ex = await Assert.ThrowsAsync<DomainException>(() =>
            handler.Handle(command, CancellationToken.None)
        );

        Assert.Equal(nameof(ExceptionCodes.UnSupportedActionType), ex.ErrorCode);
    }

    [Fact]
    public async Task Handle_ApprovalByFinalApprover_CreatesMembers()
    {
        var handler = CreateHandler();
        var currentUser = new UserDetails { Role = "NationalOfficer", Email = "officer@example.com", UserId = "officer", Name = "Test Officer" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);
        var memberRequest = new MembershipRequest(
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
        );
        var memberRequests = new List<MembershipRequest>
        {
            memberRequest
        };
        var command = new ManageMembershipRequestCommand
        {
            MembershipRequests = [new MembershipRequestViewModel{MembershipRequestId = memberRequest.Id, Action = ActionType.Approve}]
        };

        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockMemberRepo.Verify(m => m.CreateMemberAsync(It.IsAny<List<Member>>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task Handle_PartialApproval_PendingFinalApproval()
    {
        var handler = CreateHandler();
        

        var currentUser = new UserDetails { Role = "CircuitPresident", Email = "approver@example.com", UserId = "user1", Name = "John Doe" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);

        var memberRequest = new MembershipRequest(
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
        );
        var memberRequests = new List<MembershipRequest>
        {
            memberRequest
        };
        var command = new ManageMembershipRequestCommand
        {
            MembershipRequests = [new MembershipRequestViewModel{MembershipRequestId = memberRequest.Id, Action = ActionType.Approve}]
        };

        _mockMemberRequestRepo.Setup(r => r.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberRequests);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockMemberRepo.Verify(m => m.CreateMemberAsync(It.IsAny<List<Member>>()), Times.Never);
    }
}
