using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface IMemberRequestRepository
{
    Task<PendingMemberRequest> CreateMemberRequestAsync(PendingMemberRequest memberRequest);
    Task<PaginatedList<PendingMemberRequest>> GetMemberRequestsAsync(PageRequest pageRequest);
}
