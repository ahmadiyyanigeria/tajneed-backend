using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using TajneedApi.Application.Commands;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.UnitTests.Commands
{
    public class RejectMemberRequestCommandHandlerTests
    {
        private readonly Mock<IMembershipRequestRepository> _memberRequestRepositoryMock;
        private readonly Mock<IMemberRepository> _memberRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICurrentUser> _currentUserMock;
        private readonly Mock<IOptions<ApprovalSettingsConfiguration>> _approvalSettingsMock;
        private readonly Mock<ILogger<RejectMemberRequest.Handler>> _loggerMock;
        private readonly RejectMemberRequest.Handler _handler;

        public RejectMemberRequestCommandHandlerTests()
        {
            _memberRequestRepositoryMock = new Mock<IMembershipRequestRepository>();
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _currentUserMock = new Mock<ICurrentUser>();
            _approvalSettingsMock = new Mock<IOptions<ApprovalSettingsConfiguration>>();
            _loggerMock = new Mock<ILogger<RejectMemberRequest.Handler>>();

            var approvalSettingsConfigurations = Options.Create(new ApprovalSettingsConfiguration{Roles =
            [
                new RoleSettings { RoleName = "CircuitPresident", Level = 0 },
                new RoleSettings { RoleName = "NationalOfficer", Level = 1 }
            ]});

            _handler = new RejectMemberRequest.Handler(
                _memberRequestRepositoryMock.Object,
                _memberRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _currentUserMock.Object,
                approvalSettingsConfigurations,              
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldRejectMemberRequests_WhenValidRequest()
        {
            var command = new RejectMemberRequest.RejectMembershipRequestCommand
            {
                MembershipRequestIds = new List<string> { "requestId1", "requestId2" }
            };

            var user = new UserDetails
            {
                UserId = "1",
                Role = "CircuitPresident",
                Name = "Admin User",
                Email = "admin@example.com"
            };

            _currentUserMock.Setup(c => c.GetUserDetails()).Returns(user);

            var memberRequests = new List<MembershipRequest>
            {
                new MembershipRequest("Doe", "John", "NationalityId", false, "AuxId", "Middle", DateTime.UtcNow.AddYears(-20), "email@example.com", "1234567890", Sex.Male, MaritalStatus.Single, "Address", EmploymentStatus.Employed, "", "BatchRequestId", "JamaatId", DateTime.UtcNow),
                new MembershipRequest("Smith", "Jane", "NationalityId", false, "AuxId", "Middle", DateTime.UtcNow.AddYears(-25), "email2@example.com", "0987654321", Sex.Female, MaritalStatus.Married, "Address", EmploymentStatus.Employed, "", "BatchRequestId", "JamaatId", DateTime.UtcNow)
            };
    
            _memberRequestRepositoryMock.Setup(m => m.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()))
                .ReturnsAsync(memberRequests);


            var result = await _handler.Handle(command, CancellationToken.None);

            result.Succeeded.Should().BeTrue();
            result.Data.Should().HaveCount(2);
            result.Messages.First().Should().Be("Member request has been rejected.");

            _memberRequestRepositoryMock.Verify(m => m.GetMemberRequestsByIdsAsync(It.IsAny<IList<string>>(), It.IsAny<int>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowDomainException_WhenUserHasNoApprovalPermission()
        {
            var command = new RejectMemberRequest.RejectMembershipRequestCommand
            {
                MembershipRequestIds = new List<string> { "requestId1", "requestId2" }
            };

            var user = new UserDetails
            {
                UserId = "userId",
                Role = "UnauthorizedRole",
                Name = "Unauthorized User",
                Email = "unauthorized@example.com"
            };

            _currentUserMock.Setup(c => c.GetUserDetails()).Returns(user);

            var action = async () => await _handler.Handle(command, CancellationToken.None);

            await action.Should().ThrowAsync<DomainException>()
                .WithMessage("User with email unauthorized@example.com and role UnauthorizedRole does not have permission to approve requests");

            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
