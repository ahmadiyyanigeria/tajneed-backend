using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.Application.Commands;

public class ManageUpdateMemberRequest
{
    public record ManageUpdateMemberRequestCommand : IRequest<IResult<IList<UpdateMemberRequestResponse>>>
    {
        public IList<UpdateMemberRequestViewModel> UpdateMemberRequests { get; init; } = default!;
    }
    public record UpdateMemberRequestViewModel
    {
        public string MemberUpdateCaseId { get; init; } = default!;
        public string MemberId { get; init; } = default!;
        public ActionType Action { get; init; }
    }

    public class Handler(IMemberUpdateCaseRepository memberUpdateCaseRepository, IMemberRepository memberRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, IOptions<ApprovalSettingsConfiguration> approvalSettings, ILogger<Handler> logger) : IRequestHandler<ManageUpdateMemberRequestCommand, IResult<IList<UpdateMemberRequestResponse>>>
    {
        private readonly IMemberUpdateCaseRepository _memberUpdateCaseRepository = memberUpdateCaseRepository;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly ApprovalSettingsConfiguration _approvalSettings = approvalSettings.Value;
        private readonly ILogger<Handler> _logger = logger;

        public async Task<IResult<IList<UpdateMemberRequestResponse>>> Handle(ManageUpdateMemberRequestCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUser.GetUserDetails();
            var userApprovalSettings = _approvalSettings.Roles.FirstOrDefault(a => String.Equals(a.RoleName, user.Role, StringComparison.OrdinalIgnoreCase));

            if (userApprovalSettings == null)
            {
                _logger.LogError("User with email {Email} and role {Role} does not have permission to manage requests", user.Email, user.Role);
                throw new DomainException($"User with email {user.Email} and role {user.Role} does not have permission to manage requests", ExceptionCodes.AccessDeniedToApproveRequests.ToString(), 403);
            }
            var updateMemberCasesRequestActions = request.UpdateMemberRequests
                         .ToDictionary(request => request.MemberUpdateCaseId,
                                       request => request.Action);
            var memberIds = request.UpdateMemberRequests.Select(a => a.MemberId).ToList();
            var updateCases = await _memberUpdateCaseRepository.GetMemberUpdateCasesByIdsAsync(updateMemberCasesRequestActions.Keys.ToList(), userApprovalSettings.Level);
            var members = await _memberRepository.GetMembersByIdsAsync(memberIds);
            int approvedCount = 0;
            int rejectedCount = 0;

            foreach (var @case in updateCases)
            {
                var actionType = updateMemberCasesRequestActions[@case.Id];
                switch (actionType)
                {
                    case ActionType.Approve:
                        if (_approvalSettings.Roles.LastOrDefault()?.Level == userApprovalSettings.Level)
                        {
                            var member = members.FirstOrDefault(a => a.Id == @case.MemberId);
                            if (member == null)
                            {
                                _logger.LogError("Member Id {MemberId} associated with the update case member request with id {UpdateCaseId} cannot be found", @case.MemberId, @case.Id);
                                throw new DomainException($"Member Id {@case.MemberId} associated with the update case member request with id {@case.Id} cannot be found", ExceptionCodes.MemberAssociatedWithUpdateCaseNotFound.ToString(), 404);
                            }
                            member.Update(@case.BiodataUpdateCase);
                        }
                        @case.AddApprovalHistory(user.UserId, user.Role, user.Name);
                        @case.UpdateStatus(RequestStatus.Approved);
                        approvedCount++;
                        break;

                    case ActionType.Reject:
                        @case.AddDisapprovalHistory(user.UserId, user.Role, user.Name);
                        @case.UpdateStatus(RequestStatus.Rejected);
                        rejectedCount++;
                        break;

                    default:
                        throw new DomainException($"Unsupported action type", ExceptionCodes.UnSupportedActionType.ToString(), 403);
                }
            }
            _memberRepository.UpdateMembersAsync(members);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = updateCases.Adapt<IList<UpdateMemberRequestResponse>>();
            string message = $"Total requests processed: {approvedCount + rejectedCount}. Approved: {approvedCount}, Rejected: {rejectedCount}.";
            if (approvedCount > 0 && userApprovalSettings.Level < _approvalSettings.Roles.LastOrDefault()?.Level)
            {
                message += "Update Member requests are pending final approval.";
            }
            return await Result<IList<UpdateMemberRequestResponse>>.SuccessAsync(response, message);
        }
    }
    public record UpdateMemberRequestResponse(string Id, string MemberId, Status Status);

    public class ManageMembershipRequestCommandValidator : AbstractValidator<ManageUpdateMemberRequestCommand>
    {
        public ManageMembershipRequestCommandValidator()
        {
            RuleFor(x => x.UpdateMemberRequests)
                .NotNull().WithMessage("Update requests cannot be null.")
                .NotEmpty().WithMessage("Update requests cannot be empty.");

            // Apply validation for each item in the MembershipRequests list
            RuleForEach(x => x.UpdateMemberRequests).SetValidator(new MembershipRequestViewModelValidator());
        }
    }

    public class MembershipRequestViewModelValidator : AbstractValidator<UpdateMemberRequestViewModel>
    {
        public MembershipRequestViewModelValidator()
        {
            RuleFor(x => x.MemberUpdateCaseId)
                .NotEmpty().WithMessage("Membership Update Request ID is required.")
                .NotNull().WithMessage("Membership Update Request ID cannot be null.")
                .Must(id => !string.IsNullOrWhiteSpace(id)).WithMessage("Membership Update Request ID cannot be whitespace.");

            RuleFor(x => x.MemberId)
                .NotEmpty().WithMessage("Membership ID is required.")
                .NotNull().WithMessage("Membership ID cannot be null.")
                .Must(id => !string.IsNullOrWhiteSpace(id)).WithMessage("Membership ID cannot be whitespace.");

            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Invalid action type.");
        }
        
    }

}