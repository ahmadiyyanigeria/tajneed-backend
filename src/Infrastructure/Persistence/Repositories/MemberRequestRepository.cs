using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class MemberRequestRepository(ApplicationDbContext context) : IMemberRequestRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PendingMemberRequest> CreateMemberRequestAsync(PendingMemberRequest memberRequest)
    {
        await _context.AddAsync(memberRequest);
        return memberRequest;
    }
}
