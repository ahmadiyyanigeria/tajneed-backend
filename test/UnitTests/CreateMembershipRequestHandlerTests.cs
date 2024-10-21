using TajneedApi.Application.Commands;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;

public class CreateMemberRequestHandlerTests
{
    private readonly Mock<IMembershipRequestRepository> _mockMemberRequestRepository;
    private readonly Mock<IAuxiliaryBodyRepository> _mockAuxiliaryBodyRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateMemberRequest.Handler _handler;

    public CreateMemberRequestHandlerTests()
    {
        _mockMemberRequestRepository = new Mock<IMembershipRequestRepository>();
        _mockAuxiliaryBodyRepository = new Mock<IAuxiliaryBodyRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateMemberRequest.Handler(
            _mockMemberRequestRepository.Object,
            _mockAuxiliaryBodyRepository.Object,
            _mockUnitOfWork.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenValidRequestIsProvided()
    {
        var command = new CreateMemberRequest.CreateMembershipRequestCommand
        {
            Requests = new List<CreateMemberRequest.CreateMembershipRequestDto>
            {
                new CreateMemberRequest.CreateMembershipRequestDto
                {
                    Surname = "Doe",
                    FirstName = "John",
                    MiddleName = "Edward",
                    Email = "john.doe@example.com",
                    PhoneNo = "123-456-7890",
                    Dob = new DateTime(1990, 5, 15),
                    Sex = Sex.Male,
                    MaritalStatus = MaritalStatus.Single,
                    Address = "123 Main St, Anytown, USA",
                    EmploymentStatus = EmploymentStatus.Employed,
                    IsBornMember = true,
                    Occupation = "Software Developer",
                    JamaatId = "Jamaat001",
                    NationalityId = "US123456"
                }
            }
        };

        var auxiliaryBody = new AuxiliaryBody ("Atfal", Sex.Male);
        _mockAuxiliaryBodyRepository
            .Setup(repo => repo.FIndAuxiliaryBodyByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(auxiliaryBody);

        _mockMemberRequestRepository
            .Setup(repo => repo.CreateMemberRequestAsync(It.IsAny<List<MembershipRequest>>()))
            .ReturnsAsync(new List<MembershipRequest>());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Succeeded.Should().BeTrue();
        result.Messages.First().Should().Be("Member registration request submitted! Awaiting approval from Tajneed office.");
        _mockMemberRequestRepository.Verify(repo => repo.CreateMemberRequestAsync(It.IsAny<List<MembershipRequest>>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenDuplicateMemberRequestExists()
    {

        var command = new CreateMemberRequest.CreateMembershipRequestCommand
        {
            Requests = new List<CreateMemberRequest.CreateMembershipRequestDto>
            {
                new CreateMemberRequest.CreateMembershipRequestDto
                {
                    Surname = "Doe",
                    FirstName = "John",
                    MiddleName = "Edward",
                    Email = "john.doe@example.com",
                    PhoneNo = "123-456-7890",
                    Dob = new DateTime(1990, 5, 15),
                    Sex = Sex.Male,
                    MaritalStatus = MaritalStatus.Single,
                    Address = "123 Main St, Anytown, USA",
                    EmploymentStatus = EmploymentStatus.Employed,
                    IsBornMember = true,
                    Occupation = "Software Developer",
                    JamaatId = "Jamaat001",
                    NationalityId = "US123456"
                }
            }
        };

        _mockMemberRequestRepository
            .Setup(repo => repo.CreateMemberRequestAsync(It.IsAny<List<MembershipRequest>>()))
            .Throws(new Exception("Duplicate member request found"));

        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));

        _mockMemberRequestRepository.Verify(repo => repo.CreateMemberRequestAsync(It.IsAny<List<MembershipRequest>>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
