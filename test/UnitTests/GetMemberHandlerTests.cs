using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TajneedApi.Application.Queries;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Domain.Exceptions;
using TajneedApi.Domain.ValueObjects;
using Xunit;

namespace TajneedApi.UnitTests.Queries
{
    public class GetMemberQueryHandlerTests
    {
        private readonly Mock<IMemberRepository> _memberRepositoryMock;
        private readonly GetMember.Handler _handler;

        public GetMemberQueryHandlerTests()
        {
            _memberRepositoryMock = new Mock<IMemberRepository>();
            _handler = new GetMember.Handler(_memberRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnMember_WhenMemberExists()
        {
            // Arrange
            var query = new GetMember.GetMemberQuery
            {
                Id = "memberId1"
            };

            var member = new Member(
                chandaNo: "CN123456",
                membershipRequestId: "MR78910",
                aimsNo: "AIMS456",
                nextOfKinName: "John Doe",
                wasiyatNo: "WAS112233",
                spouseNo: "Jane Doe",
                recordFlag: "ACTIVE",
                fatherNo: "Father123",
                childrenNos: "Child1, Child2",
                nextOfKinPhoneNo: "555-1234"               
            )
            {
                MembershipRequest = new MembershipRequest("Doe", "John", "NationalityId", false, "AuxId", "Middle", DateTime.UtcNow.AddYears(-20), "email@example.com", "1234567890", Sex.Male, MaritalStatus.Single, "Address", EmploymentStatus.Employed, "", "BatchRequestId", "JamaatId", DateTime.UtcNow),

            };
            
            _memberRepositoryMock.Setup(m => m.GetMemberAsync(It.IsAny<string>()))
                .ReturnsAsync(member);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.MembershipRequestId.Should().Be("MR78910");
            result.Data.ChandaNo.Should().Be("CN123456");

            _memberRepositoryMock.Verify(m => m.GetMemberAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnFail_WhenMemberDoesNotExist()
        {
            // Arrange
            var query = new GetMember.GetMemberQuery
            {
                Id = "nonExistentMemberId"
            };

            _memberRepositoryMock.Setup(m => m.GetMemberAsync(query.Id))
                .ReturnsAsync((Member?)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Messages.First().Should().Be("Member request not found");

            _memberRepositoryMock.Verify(m => m.GetMemberAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
