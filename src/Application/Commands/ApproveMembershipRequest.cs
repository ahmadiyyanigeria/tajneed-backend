using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Application.Commands;

public class ApproveMembershipRequest
{
    public record ApproveMembershipRequestCommand : IRequest<IResult<IList<MembershipRequestResponse>>>
    {
        public IList<string> MembershipRequestIds { get; init; } = default!;
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository, IMemberRepository memberRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser, IOptions<ApprovalSettingsConfiguration> approvalSettings, ILogger<Handler> logger) : IRequestHandler<ApproveMembershipRequestCommand, IResult<IList<MembershipRequestResponse>>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly ApprovalSettingsConfiguration _approvalSettings = approvalSettings.Value;
        private readonly ILogger<Handler> _logger = logger;

        public async Task<IResult<IList<MembershipRequestResponse>>> Handle(ApproveMembershipRequestCommand request, CancellationToken cancellationToken)
        {
            var user = _currentUser.GetUserDetails();
            var userApprovalSettings = _approvalSettings.Roles.FirstOrDefault(a => String.Equals(a.RoleName,user.Role, StringComparison.OrdinalIgnoreCase));
            var lastLevelOfApproval = _approvalSettings.Roles.LastOrDefault()?.Level;
            if(userApprovalSettings == null)
            {
                _logger.LogError("User with email {Email} and role {Role} does not have permission to approve requests", user.Email,user.Role);
                throw new DomainException($"User with email {user.Email} and role {user.Role} does not have permission to approve requests", ExceptionCodes.AccessDeniedToApproveRequests.ToString(), 403);
            }
            var memberRequests = await _memberRequestRepository.GetMemberRequestsByIdsAsync(request.MembershipRequestIds, userApprovalSettings.Level);
            if(lastLevelOfApproval == userApprovalSettings.Level)
            {
                var members = new List<Member>();
                foreach (var memberRequest in memberRequests)
                {
                    members.Add(new Member(Guid.NewGuid().ToString(),memberRequest.Id));
                    memberRequest.AddApprovalHistory(user.UserId, user.Role, user.Name);
                    memberRequest.UpdateRequestStatus(RequestStatus.Approved);
                }
                var memberRequestResponse = await _memberRepository.CreateMemberAsync(members);
            }
            else
            {
                foreach (var memberRequest in memberRequests)
                {
                    memberRequest.AddApprovalHistory(user.UserId, user.Role, user.Name);
                }
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            var response = memberRequests.Adapt<IList<MembershipRequestResponse>>();
            return await Result<IList<MembershipRequestResponse>>.SuccessAsync(response, $"Member request has been approved by {user.Name}. {(lastLevelOfApproval > userApprovalSettings.Level ? "Member requests pending final approval" : "")}");
        }
        
    }

    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);

    public class ApproveMemberRequestCommandValidator : AbstractValidator<ApproveMembershipRequestCommand>
    {
        public ApproveMemberRequestCommandValidator()
        {
            RuleFor(x => x.MembershipRequestIds)
                .NotNull().WithMessage("MembershipRequestIds cannot be null.")
                .Must(x => x != null && x.Any()).WithMessage("MembershipRequestIds must contain at least one item.");       
        }
    }
}