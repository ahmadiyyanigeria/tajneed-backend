using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using static TajneedApi.Application.Commands.User.CreateMemberRequest;

namespace TajneedApi.Application.Repositories;

public interface IMemberRequestRepository
{
    Task<PendingMemberRequest> CreateMemberRequestAsync(PendingMemberRequest memberRequest);
    Task<PendingMemberRequest?> GetMemberRequestAsync(string id);
    Task<PaginatedList<PendingMemberRequest>> GetMemberRequestsAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null);
    Task<bool> IsDuplicateMemberRequestAsync(CreateMemberRequestCommand newMember);
}
