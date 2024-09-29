using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Configurations;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.Application.Commands
{
    public class ManageMembershipRequest
    {
        public record ManageMembershipRequestCommand : IRequest<IResult<IList<MembershipRequestResponse>>>
        {
            public IList<string> MembershipRequestIds { get; init; } = default!;
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

                var memberRequests = await _memberRequestRepository.GetMemberRequestsByIdsAsync(request.MembershipRequestIds, userApprovalSettings.Level);

                switch (request.Action)
                {
                    case ActionType.Approve:
                        if (_approvalSettings.Roles.LastOrDefault()?.Level == userApprovalSettings.Level)
                        {
                            var members = new List<Member>();
                            foreach (var memberReq in memberRequests)
                            {
                                members.Add(new Member(Guid.NewGuid().ToString(), memberReq.Id));
                                memberReq.AddApprovalHistory(user.UserId, user.Role, user.Name);
                                memberReq.UpdateRequestStatus(RequestStatus.Approved);
                            }
                            await _memberRepository.CreateMemberAsync(members);
                        }
                        else
                        {
                            foreach (var memberReq in memberRequests)
                            {
                                memberReq.AddApprovalHistory(user.UserId, user.Role, user.Name);
                            }
                        }
                        break;
                    case ActionType.Reject:
                        foreach (var memberReq in memberRequests)
                        {
                            memberReq.AddDisapprovalHistory(user.UserId, user.Role, user.Name);
                            memberReq.UpdateRequestStatus(RequestStatus.Rejected);
                        }
                        break;
                    default:
                        throw new DomainException($"Unsupported action type", ExceptionCodes.UnSupportedActionType.ToString(), 403);
                }
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                var response = memberRequests.Adapt<IList<MembershipRequestResponse>>();
                string message;
                if (request.Action == ActionType.Approve)
                {
                    message = $"Member request has been approved by {user.Name}. {(userApprovalSettings.Level < _approvalSettings.Roles.LastOrDefault()?.Level ? "Member requests pending final approval" : "")}";
                }
                else
                {
                    message = $"Member request has been rejected.";
                }
                return await Result<IList<MembershipRequestResponse>>.SuccessAsync(response, message);
            }
        }
        public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);

        public class ApproveMemberRequestCommandValidator : AbstractValidator<ManageMembershipRequestCommand>
        {
            public ApproveMemberRequestCommandValidator()
            {
                RuleFor(x => x.MembershipRequestIds)
                    .NotNull().WithMessage("MembershipRequestIds cannot be null.")
                    .Must(x => x != null && x.Any()).WithMessage("MembershipRequestIds must contain at least one item.");

                RuleFor(x => x.Action)
                    .NotNull().WithMessage("Action cannot be null.")
                    .IsInEnum().WithMessage("Action must be a valid enum value.");
            }
        }
    }
}