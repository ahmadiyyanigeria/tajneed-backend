using TajneedApi.Domain.Entities.CaseAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Application.Commands;

public class UpdateMemberRequest
{
    public record UpdateMemberRequestCommand : IRequest<IResult<UpdateMemberRequestResponse>>
    {
        public string MemberId { get; init; } = default!;
        public BiodataUpdateCase? BiodataUpdateCase { get; init; }
        public RelocationCase? RelocationCase { get; init; } 
        public DuplicateAccountCase? DuplicateAccountCase { get; init; }
    }

    public class Handler(IMemberUpdateCaseRepository caseRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateMemberRequestCommand, IResult<UpdateMemberRequestResponse>>
    {
        private readonly IMemberUpdateCaseRepository _caseRepository = caseRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IResult<UpdateMemberRequestResponse>> Handle(UpdateMemberRequestCommand request, CancellationToken cancellationToken)
        {
            var caseRequest  = new MemberUpdateCase(request.MemberId, request.BiodataUpdateCase, request.DuplicateAccountCase, request.RelocationCase);
            var caseRequestResponse = await _caseRepository.CreateCaseAsync(caseRequest);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = caseRequestResponse.Adapt<UpdateMemberRequestResponse>();
            return await Result<UpdateMemberRequestResponse>.SuccessAsync(response, "Member update request submitted! Awaiting approval from Tajneed office.");
        }

      
    }

    public record UpdateMemberRequestResponse(string Id, string MemberId, Status Status);

    public class UpdateMemberRequestCommandValidator : AbstractValidator<UpdateMemberRequestCommand>
    {
        public UpdateMemberRequestCommandValidator()
        {

            RuleFor(x => x.MemberId)
                .NotEmpty().WithMessage("Member ID is required.");

            RuleFor(x => x)
                .Must(HaveAtLeastOneCase).WithMessage("At least one case type (BiodataUpdateCase, RelocationCase, or DuplicateAccountCase) must be provided.");
        }
        private bool HaveAtLeastOneCase(UpdateMemberRequestCommand request)
        {
            return request.BiodataUpdateCase != null || request.RelocationCase != null || request.DuplicateAccountCase != null;
        }
    }
}