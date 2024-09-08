using Application.Repositories;
using Domain.Entities.MemberAggregateRoot;

namespace Infrastructure.Persistence.Repositories;

public class MemberRequestRepository(ApplicationDbContext context) : IMemberRequestRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<MemberRequest> CreateMemberRequestAsync(MemberRequest memberRequest)
    {
        await _context.AddAsync(memberRequest);
        return memberRequest;
    }
}

