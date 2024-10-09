using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Application.Queries;
public class GetMembershipRequests
{
    public record GetMembershipRequestsQuery : PageRequest, IRequest<PaginatedList<IGrouping<string, MembershipRequestResponse>>>
    {
        public string? JamaatId { get; set; } = default;
        public string? CircuitId { get; set; } = default;
        public RequestStatus? RequestStatus { get; set; } = default;
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository) : IRequestHandler<GetMembershipRequestsQuery, PaginatedList<IGrouping<string, MembershipRequestResponse>>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;

        public async Task<PaginatedList<IGrouping<string, MembershipRequestResponse>>> Handle(GetMembershipRequestsQuery request, CancellationToken cancellationToken)
        {
            var memberRequests = await _memberRequestRepository.GetMemberRequestsAsync(request, request.JamaatId, request.CircuitId, request.RequestStatus);
            //TODO: investigate why the response does not contian LastModifiedBy 
            return memberRequests.Adapt<PaginatedList<IGrouping<string, MembershipRequestResponse>>>();
        }
    }

    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
