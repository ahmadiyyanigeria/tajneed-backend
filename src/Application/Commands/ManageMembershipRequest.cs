using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Configurations;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.Application.Commands;

public class ManageMembershipRequest
{
    public record ManageMembershipRequestCommand : IRequest<IResult<IList<MembershipRequestResponse>>>
    {
        public IList<MembershipRequestViewModel> MembershipRequests { get; init; } = default!;
    }
    public record MembershipRequestViewModel
    {
        public string MembershipRequestId { get; init; } = default!;
        public ActionType Action { get; init; }
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository, IMemberRepository memberRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, IOptions<ApprovalSettingsConfiguration> approvalSettings, ILogger<Handler> logger) : IRequestHandler<ManageMembershipRequestCommand, IResult<IList<MembershipRequestResponse>>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly ApprovalSettingsConfiguration _approvalSettings = approvalSettings.Value;
        private readonly ILogger<Handler> _logger = logger;

        public async Task<IResult<IList<MembershipRequestResponse>>> Handle(ManageMembershipRequestCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUser.GetUserDetails();
            var userApprovalSettings = _approvalSettings.Roles.FirstOrDefault(a => String.Equals(a.RoleName, user.Role, StringComparison.OrdinalIgnoreCase));

            if (userApprovalSettings == null)
            {
                _logger.LogError("User with email {Email} and role {Role} does not have permission to manage requests", user.Email, user.Role);
                throw new DomainException($"User with email {user.Email} and role {user.Role} does not have permission to manage requests", ExceptionCodes.AccessDeniedToApproveRequests.ToString(), 403);
            }
            var membershipRequestActions = request.MembershipRequests
                         .ToDictionary(membershipRequest => membershipRequest.MembershipRequestId,
                                       membershipRequest => membershipRequest.Action);
            var memberRequests = await _memberRequestRepository.GetMemberRequestsByIdsAsync(membershipRequestActions.Keys.ToList(), userApprovalSettings.Level);
            var members = new List<Member>();
            int approvedCount = 0;
            int rejectedCount = 0;

            foreach (var memberReq in memberRequests)
            {
                var actionType = membershipRequestActions[memberReq.Id];
                switch (actionType)
                {
                    case ActionType.Approve:
                        if (_approvalSettings.Roles.LastOrDefault()?.Level == userApprovalSettings.Level)
                        {
                            members.Add(new Member(Guid.NewGuid().ToString(), memberReq.Id));
                        }
                        memberReq.AddApprovalHistory(user.UserId, user.Role, user.Name);
                        memberReq.UpdateRequestStatus(RequestStatus.Approved);
                        approvedCount++;
                        break;

                    case ActionType.Reject:
                        memberReq.AddDisapprovalHistory(user.UserId, user.Role, user.Name);
                        memberReq.UpdateRequestStatus(RequestStatus.Rejected);
                        rejectedCount++;
                        break;

                    default:
                        throw new DomainException($"Unsupported action type", ExceptionCodes.UnSupportedActionType.ToString(), 403);
                }
            }
            if (members.Count > 0)
            {
                await _memberRepository.CreateMemberAsync(members);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = memberRequests.Adapt<IList<MembershipRequestResponse>>();
            string message = $"Total requests processed: {approvedCount + rejectedCount}. Approved: {approvedCount}, Rejected: {rejectedCount}.";
            if (approvedCount > 0 && userApprovalSettings.Level < _approvalSettings.Roles.LastOrDefault()?.Level)
            {
                message += " Member requests are pending final approval.";
            }
            return await Result<IList<MembershipRequestResponse>>.SuccessAsync(response, message);
        }
    }
    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);

    public class ManageMembershipRequestCommandValidator : AbstractValidator<ManageMembershipRequestCommand>
    {
        public ManageMembershipRequestCommandValidator()
        {
            RuleFor(x => x.MembershipRequests)
                .NotNull().WithMessage("Membership requests cannot be null.")
                .NotEmpty().WithMessage("Membership requests cannot be empty.");

            // Apply validation for each item in the MembershipRequests list
            RuleForEach(x => x.MembershipRequests).SetValidator(new MembershipRequestViewModelValidator());
        }
    }

    public class MembershipRequestViewModelValidator : AbstractValidator<MembershipRequestViewModel>
    {
        public MembershipRequestViewModelValidator()
        {
            RuleFor(x => x.MembershipRequestId)
                .NotEmpty().WithMessage("Membership Request ID is required.")
                .NotNull().WithMessage("Membership Request ID cannot be null.")
                .Must(id => !string.IsNullOrWhiteSpace(id)).WithMessage("Membership Request ID cannot be whitespace.");

            RuleFor(x => x.Action)
                .IsInEnum().WithMessage("Invalid action type.");
        }
    }

}