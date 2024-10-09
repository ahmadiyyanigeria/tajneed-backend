﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.Application.Queries;
public class GetToBeApprovedMembershipRequests
{
    public record GetToBeApprovedMembershipRequestsQuery : PageRequest, IRequest<PaginatedList<IGrouping<string, MembershipRequestResponse>>>
    {
        public string? JamaatId { get; set; } = default;
        public string? CircuitId { get; set; } = default;
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository, ICurrentUser currentUser, IOptions<ApprovalSettingsConfiguration> approvalSettings, ILogger<Handler> logger) : IRequestHandler<GetToBeApprovedMembershipRequestsQuery, PaginatedList<IGrouping<string, MembershipRequestResponse>>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly ApprovalSettingsConfiguration _approvalSettings = approvalSettings.Value;
        private readonly ILogger<Handler> _logger = logger;

        public async Task<PaginatedList<IGrouping<string, MembershipRequestResponse>>> Handle(GetToBeApprovedMembershipRequestsQuery request, CancellationToken cancellationToken)
        {
            var user = _currentUser.GetUserDetails();
            var userApprovalSettings = _approvalSettings.Roles.FirstOrDefault(a => String.Equals(a.RoleName,user.Role, StringComparison.OrdinalIgnoreCase));
            if(userApprovalSettings == null)
            {
                _logger.LogError("User with email {Email} and role {Role} does not have permission to approve requests", user.Email,user.Role);
                throw new DomainException($"User with email {user.Email} and role {user.Role} does not have permission to approve requests", ExceptionCodes.AccessDeniedToApproveRequests.ToString(), 403);
            }
            var memberRequests = await _memberRequestRepository.GetToBeApprovedMembershipRequestsAsync(request, userApprovalSettings.Level, request.JamaatId, request.CircuitId);
            return memberRequests.Adapt<PaginatedList<IGrouping<string, MembershipRequestResponse>>>();

        }
    }
    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}