using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Commands;
using TajneedApi.Application.Configurations;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;
using TajneedApi.Application.Repositories;
using static TajneedApi.Application.Commands.ManageUpdateMemberRequest;
using TajneedApi.Domain.Entities.CaseAggregateRoot;
using TajneedApi.Domain.ValueObjects;
// using static TajneedApi.Application.Commands.ManageMembershipRequest;

public class ManageUpdateMemberRequestHandlerTests
{
    private readonly Mock<IMemberUpdateCaseRepository> _mockUpdateMemberCaseRepo;
    private readonly Mock<IMemberRepository> _mockMemberRepo;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ICurrentUser> _mockCurrentUser;
    private readonly Mock<IOptions<ApprovalSettingsConfiguration>> _mockApprovalSettings;
    private readonly Mock<ILogger<Handler>> _mockLogger;

    public ManageUpdateMemberRequestHandlerTests()
    {
        _mockUpdateMemberCaseRepo = new Mock<IMemberUpdateCaseRepository>();
        _mockMemberRepo = new Mock<IMemberRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockCurrentUser = new Mock<ICurrentUser>();
        _mockApprovalSettings = new Mock<IOptions<ApprovalSettingsConfiguration>>();
        _mockLogger = new Mock<ILogger<ManageUpdateMemberRequest.Handler>>();
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
        return new Handler(
            _mockUpdateMemberCaseRepo.Object,
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
        var command = new ManageUpdateMemberRequestCommand
        {
            UpdateMemberRequests = [new UpdateMemberRequestViewModel{MemberUpdateCaseId = "req1"}]
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
        
        var memberUpdateCases = GetMemberUpdateCasesData();
        var command = new ManageUpdateMemberRequestCommand
        {
            UpdateMemberRequests = [new UpdateMemberRequestViewModel{MemberUpdateCaseId = memberUpdateCases.First().Id, MemberId = memberUpdateCases.First().MemberId, Action = ActionType.Reject}]
        };

        _mockUpdateMemberCaseRepo.Setup(r => r.GetMemberUpdateCasesByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberUpdateCases);

        _mockMemberRepo.Setup(r => r.GetMembersByIdsAsync(It.IsAny<IList<string>>())).ReturnsAsync(GetMembersData());

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(RequestStatus.Rejected, memberUpdateCases[0].RequestStatus);
    }

    [Fact]
    public async Task Handle_UnsupportedActionType_ThrowsDomainException()
    {
        var handler = CreateHandler();
        var memberUpdateCases = GetMemberUpdateCasesData();
        var command = new ManageUpdateMemberRequestCommand
        {
            UpdateMemberRequests = [new UpdateMemberRequestViewModel{MemberUpdateCaseId = memberUpdateCases.First().Id, MemberId = memberUpdateCases.First().MemberId, Action = (ActionType)10}]
        };

        _mockUpdateMemberCaseRepo.Setup(r => r.GetMemberUpdateCasesByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberUpdateCases);
        var currentUser = new UserDetails { Role = "NationalOfficer", Email = "officer@example.com" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);
        _mockMemberRepo.Setup(r => r.GetMembersByIdsAsync(It.IsAny<IList<string>>())).ReturnsAsync(GetMembersData());

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
        var memberUpdateCases = GetMemberUpdateCasesData();
        var command = new ManageUpdateMemberRequestCommand
        {
            UpdateMemberRequests = [new UpdateMemberRequestViewModel{MemberUpdateCaseId = memberUpdateCases.First().Id, MemberId = memberUpdateCases.First().MemberId, Action = ActionType.Approve}]
        };

        _mockUpdateMemberCaseRepo.Setup(r => r.GetMemberUpdateCasesByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberUpdateCases);
        _mockMemberRepo.Setup(r => r.GetMembersByIdsAsync(It.IsAny<IList<string>>())).ReturnsAsync(GetMembersData());

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockMemberRepo.Verify(m => m.UpdateMembersAsync(It.IsAny<List<Member>>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact]
    public async Task Handle_PartialApproval_PendingFinalApproval()
    {
        var handler = CreateHandler();
        

        var currentUser = new UserDetails { Role = "CircuitPresident", Email = "approver@example.com", UserId = "user1", Name = "John Doe" };
        _mockCurrentUser.Setup(c => c.GetUserDetails()).Returns(currentUser);

        var memberUpdateCases = GetMemberUpdateCasesData();
        var command = new ManageUpdateMemberRequestCommand
        {
            UpdateMemberRequests = [new UpdateMemberRequestViewModel{MemberUpdateCaseId = memberUpdateCases.First().Id, MemberId = memberUpdateCases.First().MemberId, Action = ActionType.Reject}]
        };

        _mockUpdateMemberCaseRepo.Setup(r => r.GetMemberUpdateCasesByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
            .ReturnsAsync(memberUpdateCases);
        _mockMemberRepo.Setup(r => r.GetMembersByIdsAsync(It.IsAny<IList<string>>())).ReturnsAsync(GetMembersData());

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.Succeeded);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockMemberRepo.Verify(m => m.UpdateMembersAsync(It.IsAny<List<Member>>()), Times.Once);
    }

    private static List<MemberUpdateCase> GetMemberUpdateCasesData()
    {
        var biodataUpdateCase = new BiodataUpdateCase(
            surName: "Doe",
            firstName: "John",
            middleName: "M",
            dob: new DateTime(1990, 5, 15),
            address: "123 Main Street",
            email: "johndoe@example.com",
            phoneNumber: "1234567890",
            employmentStatus: EmploymentStatus.Employed,
            jamaatId: "J001",
            maritalStatus: MaritalStatus.Single,
            membershipStatusId: "MS001",
            title: "Mr.",
            isBornMember: false,
            nationalityId: "N001",
            householdMemberId: "HH001",
            sex: Sex.Male,
            maidenName: string.Empty,
            notes: "No additional notes",
            spouseNo: null,
            fatherNo: "F001",
            childrenNos: null,
            biometricId: "BIO001",
            nextOfKinPhoneNo: "9876543210",
            occupation: "Software Engineer",
            biatDate: new DateTime(2010, 1, 1)
        );

        var duplicateAccountCase = new DuplicateAccountCase(
            primaryAccount: "PA001",
            otherAccounts: "PA002,PA003",
            notes: "Duplicate accounts detected"
        );

        var relocationCase = new RelocationCase(
            oldJamaatId: "J001",
            newJamaatId: "J002",
            notes: "Relocation due to job transfer"
        );

        var memberUpdateCase = new MemberUpdateCase(
            memberId: "4fb32efd-2b92-4308-81be-b879447d80fa",
            biodataUpdateCase: biodataUpdateCase,
            duplicateAccountCase: duplicateAccountCase,
            relocationCase: relocationCase
        );
        return [memberUpdateCase];

    }
    private static List<Member> GetMembersData()
    {
        var members = new List<Member>
        {
            new("ChandaNo1", "MR123"),
            new("ChandaNo2", "MR124"),
        };
        members.First().Id = "4fb32efd-2b92-4308-81be-b879447d80fa";
        members.First().MembershipRequest = new MembershipRequest(
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
        return members;
    }
}
