using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface IMemberRepository
{
    Task<IList<Member>> CreateMemberAsync(IList<Member> members);
    Task<Member?> GetMemberAsync(string id);
    Task<PaginatedList<Member>> GetMembersAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null);
    
}
