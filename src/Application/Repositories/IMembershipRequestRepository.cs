using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface IMembershipRequestRepository
{
    Task<IList<MembershipRequest>> CreateMemberRequestAsync(IList<MembershipRequest> memberRequest);
    Task<MembershipRequest?> GetMemberRequestAsync(string id);
    Task<PaginatedList<IGrouping<string, MembershipRequest>>> GetMemberRequestsAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null, RequestStatus? requestStatus = null);
    Task<PaginatedList<IGrouping<string, MembershipRequest>>> GetToBeApprovedMembershipRequestsAsync(PageRequest pageRequest, int approvalLevel, string? jamaatId = null, string? circuitId = null);
    Task<IList<MembershipRequest>> GetMemberRequestsByIdsAsync(IList<string> ids, int approvalLevel);
    Task<IList<MembershipRequest>> RecommendPossibleDuplicateMemberRequestAsync(IList<MembershipRequest> memberRequest);

}
