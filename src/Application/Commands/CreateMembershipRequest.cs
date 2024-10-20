using System.ComponentModel;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Application.Commands;

public class CreateMemberRequest
{
    public record CreateMembershipRequestCommand : IRequest<IResult<IList<MembershipRequestResponse>>>
    {
        public IReadOnlyList<CreateMembershipRequestDto> Requests { get; init; } = default!;
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository, IAuxiliaryBodyRepository auxiliaryBodyRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateMembershipRequestCommand, IResult<IList<MembershipRequestResponse>>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAuxiliaryBodyRepository _auxiliaryBodyRepository = auxiliaryBodyRepository;

        public async Task<IResult<IList<MembershipRequestResponse>>> Handle(CreateMembershipRequestCommand request, CancellationToken cancellationToken)
        {
        
            var batchRequestId = Guid.NewGuid().ToString();
            var memberRequests = request.Requests.Select(x => new MembershipRequest(x.Surname, x.FirstName, x.NationalityId,x.IsBornMember, GetAuxiliaryBodyId(x.Dob, x.Sex), x.MiddleName, x.Dob, x.Email, x.PhoneNo, x.Sex, x.MaritalStatus, x.Address, x.EmploymentStatus,x.Occupation,batchRequestId,x.JamaatId,x.BiatDate)).ToList();
            var memberRequestResponse = await _memberRequestRepository.CreateMemberRequestAsync(memberRequests);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = memberRequestResponse.Adapt<IList<MembershipRequestResponse>>();
            return await Result<IList<MembershipRequestResponse>>.SuccessAsync(response, "Member registration request submitted! Awaiting approval from Tajneed office.");
        }

        private string GetAuxiliaryBodyId(DateTime dob, Sex sex)
        {
            var auxiliaryBodyName = ServiceHelper.GetAuxiliaryBody(dob, sex);
            var auxiliaryBody = _auxiliaryBodyRepository.FIndAuxiliaryBodyByNameAsync(auxiliaryBodyName)?.Result;
            return auxiliaryBody?.Id;
        }
    }

    public record CreateMembershipRequestDto
    {
        [DefaultValue("aabdulsalam@gmail.com")]
        public string Email { get; init; } = default!;
        [DefaultValue("Ahmad")]
        public string FirstName { get; init; } = default!;
        [DefaultValue("Abdulsalam")]
        public string MiddleName { get; init; } = default!;
        [DefaultValue("Adnan")]
        public string Surname { get; init; } = default!;
        [DefaultValue("+2348164671994")]
        public string PhoneNo { get; init; } = default!;
        [DefaultValue("8027038c-5d2e-4368-b0a9-49609dc80a80")]
        public string JamaatId { get; init; } = default!;
        [DefaultValue("6b Zone 2, Lagos Ibadan express way.")]
        public string Address { get; init; } = default!;
        public bool IsBornMember { get; init; }
        public string Occupation { get; init; } = default!;
        public DateTime? BiatDate { get; init ; }
        public string NationalityId { get; init; } = default!;
        public DateTime Dob { get; init; }
        public Sex Sex { get; init; }
        public MaritalStatus MaritalStatus { get; init; }
        public EmploymentStatus EmploymentStatus { get; init; }
    }

    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);

    public class CreateMemberRequestCommandValidator : AbstractValidator<CreateMembershipRequestCommand>
    {
        public CreateMemberRequestCommandValidator()
        {
            RuleFor(x => x.Requests).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("At least one member request is required.");


            When(p => p.Requests is not null, () =>
            {
                RuleForEach(x => x.Requests)
                .ChildRules(p =>
                {
                    p.RuleFor(x => x.JamaatId)
                    .NotEmpty().WithMessage("Jamaat Id must be selected");

                    p.RuleFor(x => x.FirstName)
                    .NotEmpty().WithMessage("First Name is required");

                    p.RuleFor(x => x.Surname)
                    .NotEmpty().WithMessage("Surname is required");

                    p.RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Invalid email format");

                    p.RuleFor(x => x.PhoneNo)
                    .NotEmpty().WithMessage("Phone number is required")
                    .MaximumLength(14);

                    p.RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required");

                    p.RuleFor(x => x.Dob)
                    .NotEmpty().WithMessage("Date of birth is required")
                    .Must(x => x <= DateTime.Today).WithMessage("Date of birth cannot be earlier than today.");

                    p.RuleFor(x => x.BiatDate)
                    .NotEmpty().When(x => !x.IsBornMember).WithMessage("Biat Date is required for non-born members.");

                    p.RuleFor(x => x.NationalityId)
                    .NotEmpty().WithMessage("Nationality is required");

                    p.RuleFor(x => x.Occupation)
                    .NotEmpty().WithMessage("Occupation is required");
                });
            });
        }
    }
}