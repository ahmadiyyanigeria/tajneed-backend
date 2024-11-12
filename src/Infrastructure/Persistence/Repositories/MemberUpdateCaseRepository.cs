using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.CaseAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class MemberUpdateCaseRepository(ApplicationDbContext context) : IMemberUpdateCaseRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<MemberUpdateCase> CreateCaseAsync(MemberUpdateCase @case)
    {
        await _context.AddRangeAsync(@case);
        return @case;
    }
   
}
