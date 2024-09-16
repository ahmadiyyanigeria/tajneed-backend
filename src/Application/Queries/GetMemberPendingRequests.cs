using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Application.Queries;
public class GetMemberPendingRequests
{
    public record GetMemberRequestsQuery : PageRequest, IRequest<PaginatedList<MemberRequestResponse>>
    {
        public string? JamaatId { get; set; } = default;
        public string? CircuitId { get; set; } = default;
        //status
    }

    public class Handler(IMemberRequestRepository memberRequestRepository) : IRequestHandler<GetMemberRequestsQuery, PaginatedList<MemberRequestResponse>>
    {
        private readonly IMemberRequestRepository _memberRequestRepository = memberRequestRepository;

        public async Task<PaginatedList<MemberRequestResponse>> Handle(GetMemberRequestsQuery request, CancellationToken cancellationToken)
        {
            var memberRequests = await _memberRequestRepository.GetMemberRequestsAsync(request, request.JamaatId, request.CircuitId);
            //TODO: investigate why the response does not contian LastModifiedBy 
            return memberRequests.Adapt<PaginatedList<MemberRequestResponse>>();
        }
    }

    public record MemberRequestResponse(string Id, IReadOnlyList<MembershipInfoResponse> Requests, DateTime CreatedOn, string CreatedBy, string LastModifiedBy, DateTime LastModifiedOn);

    public record MembershipInfoResponse(string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, DateTime Dob, string Email, string PhoneNo, string JamaatId, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
