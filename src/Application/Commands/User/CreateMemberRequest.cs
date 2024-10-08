﻿using System.ComponentModel;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Application.Commands.User;

public class CreateMemberRequest
{
    public record CreateMemberRequestCommand : IRequest<IResult<MemberRequestResponse>>
    {
        public string JamaatId { get; init; } = default!;
        public IReadOnlyList<CreateMemberRequestDto> Requests { get; init; } = default!;
    }

    public class Handler(IMemberRequestRepository memberRequestRepository, IAuxiliaryBodyRepository auxiliaryBodyRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateMemberRequestCommand, IResult<MemberRequestResponse>>
    {
        private readonly IMemberRequestRepository _memberRequestRepository = memberRequestRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IAuxiliaryBodyRepository _auxiliaryBodyRepository = auxiliaryBodyRepository;

        public async Task<IResult<MemberRequestResponse>> Handle(CreateMemberRequestCommand request, CancellationToken cancellationToken)
        {
            //TODO: Check if data already exist. How're we confirming if a record already exist
            var requestExist = await _memberRequestRepository.IsDuplicateMemberRequestAsync(request);
            if (requestExist)
                return await Result<MemberRequestResponse>.FailAsync("There is already a pending request from a member with similar information.");

            var memberRequests = request.Requests.Select(x => new MembershipInfo(x.Surname, x.FirstName, GetAuxiliaryBodyId(x.Dob, x.Sex), x.MiddleName, x.Dob, x.Email, x.PhoneNo, x.Sex, x.MaritalStatus, x.Address, x.EmploymentStatus)).ToList();

            var pendingMemberRequest = new PendingMemberRequest(request.JamaatId, memberRequests);
            var memberRequestResponse = await _memberRequestRepository.CreateMemberRequestAsync(pendingMemberRequest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = memberRequestResponse.Adapt<MemberRequestResponse>();
            return await Result<MemberRequestResponse>.SuccessAsync(response, "Member registration request submitted! Awaiting approval from Tajneed office.");
        }

        private string GetAuxiliaryBodyId(DateTime dob, Sex sex)
        {
            var auxiliaryBodyName = ServiceHelper.GetAuxiliaryBody(dob, sex);
            var auxiliaryBody = _auxiliaryBodyRepository.FIndAuxiliaryBodyByNameAsync(auxiliaryBodyName)?.Result;
            return auxiliaryBody?.Id;
        }
    }

    public record CreateMemberRequestDto
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
        //[DefaultValue("8027038c-5d2e-4368-b0a9-49609dc80a80")]
        //public string JamaatId { get; init; } = default!;
        [DefaultValue("6b Zone 2, Lagos Ibadan express way.")]
        public string Address { get; init; } = default!;
        public DateTime Dob { get; init; }
        public Sex Sex { get; init; }
        public MaritalStatus MaritalStatus { get; init; }
        public EmploymentStatus EmploymentStatus { get; init; }
    }

    public record MemberRequestResponse(string Id, string JamaatId, RequestStatus RequestStatus);

    public class CreateMemberRequestCommandValidator : AbstractValidator<CreateMemberRequestCommand>
    {
        public CreateMemberRequestCommandValidator()
        {
            RuleFor(x => x.Requests).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("At least one member request is required.");

            RuleFor(x => x.JamaatId)
            .NotEmpty().WithMessage("Jamaat Id must be selected");

            When(p => p.Requests is not null, () =>
            {
                RuleForEach(x => x.Requests)
                .ChildRules(p =>
                {
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
                });
            });
        }
    }
}