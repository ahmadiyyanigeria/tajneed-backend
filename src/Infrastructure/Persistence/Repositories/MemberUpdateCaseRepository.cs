using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.CaseAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class MemberUpdateCaseRepository(ApplicationDbContext context) : IMemberUpdateCaseRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<MemberUpdateCase> CreateCaseAsync(MemberUpdateCase @case)
    {
        await _context.AddRangeAsync(@case);
        return @case;
    }

    public async Task<IList<MemberUpdateCase>> GetMemberUpdateCasesByIdsAsync(IList<string> ids, int approvalLevel)
    {
        return await _context.MemberUpdateCases.Where(a => ids.Contains(a.Id) && a.ApprovalHistories.Count == approvalLevel).ToListAsync();
    }

}
