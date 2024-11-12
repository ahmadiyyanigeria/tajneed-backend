using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TajneedApi.Application.Commands;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.CaseAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.ValueObjects;
using Xunit;
using static TajneedApi.Application.Commands.UpdateMemberRequest;

namespace TajneedApi.Application.Tests.Commands
{
    public class UpdateMemberRequestHandlerTests
    {
        private readonly Mock<IMemberUpdateCaseRepository> _caseRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Handler _handler;

        public UpdateMemberRequestHandlerTests()
        {
            _caseRepositoryMock = new Mock<IMemberUpdateCaseRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new Handler(_caseRepositoryMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateCaseAndReturnSuccess_WhenValidRequest()
        {
            // Arrange
            var request = new UpdateMemberRequestCommand
            {
                MemberId = "member123",
                RelocationCase = new RelocationCase("jamaat123","jamaatnew123")
            };

            _caseRepositoryMock.Setup(x => x.CreateCaseAsync(It.IsAny<MemberUpdateCase>()))
                .ReturnsAsync(new MemberUpdateCase( "member123", null, null, null));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.MemberId.Should().Be("member123");
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        
    }
}