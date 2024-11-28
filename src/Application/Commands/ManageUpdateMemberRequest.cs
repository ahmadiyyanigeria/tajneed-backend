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
            var duplicateMembersIds = new List<string>();
            if (updateCases.Count > 0 && updateCases.Any(a => a.DuplicateAccountCase != null))
            {
                var updateCasesWithDuplicateAccount = updateCases.Where(a => a.DuplicateAccountCase != null);
                foreach (var @case in updateCasesWithDuplicateAccount)
                {
                    duplicateMembersIds.Add(@case.DuplicateAccountCase.OtherAccounts);
                }
            }
            var duplicateMembersAccount = await _memberRepository.GetMembersByIdsAsync(duplicateMembersIds);
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

                            if (@case.DuplicateAccountCase != null)
                            {
                                var duplicateAccount = duplicateMembersAccount.FirstOrDefault(a => a.Id == @case.DuplicateAccountCase.OtherAccounts);
                                if (duplicateAccount == null)
                                {
                                    _logger.LogError("Duplicate account with Id {OtherAccountId} linked to update case Id {UpdateCaseId} for member Id {MemberId} cannot be found.", @case.DuplicateAccountCase.OtherAccounts, @case.Id, @case.MemberId);                                    
                                    throw new DomainException($"Duplicate account with Id '{@case.DuplicateAccountCase.OtherAccounts}' linked to update case Id '{@case.Id}' for member Id '{@case.MemberId}' cannot be found.", ExceptionCodes.MemberAssociatedWithUpdateCaseNotFound.ToString(), 404);                            
                                }
                                else if (@case.MemberId != @case.DuplicateAccountCase.PrimaryAccount)
                                {
                                    _logger.LogError("Mismatch: Member Id {MemberId} does not match the PrimaryAccount {PrimaryAccount} for update case id {UpdateCaseId}.", @case.MemberId, @case.DuplicateAccountCase.PrimaryAccount, @case.Id);
                                    throw new DomainException($"Member Id {@case.MemberId} does not match the PrimaryAccount {@case.DuplicateAccountCase.PrimaryAccount} for update case id {@case.Id}.", ExceptionCodes.MemberMismatchWithPrimaryAccount.ToString(), 400);
                                }
                                _memberRepository.Delete(duplicateAccount);
                            }

                            if (@case.BiodataUpdateCase != null)
                            {
                                member.UpdateBiodata(@case.BiodataUpdateCase);
                            }

                            if (@case.RelocationCase != null)
                            {
                                member.UpdateLocation(@case.RelocationCase);
                            }
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

    public class ManageUpdateMemberRequestCommandValidator : AbstractValidator<ManageUpdateMemberRequestCommand>
    {
        public ManageUpdateMemberRequestCommandValidator()
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