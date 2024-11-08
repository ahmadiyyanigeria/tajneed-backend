using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.CaseAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class CaseRepository(ApplicationDbContext context) : ICaseRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Case> CreateCaseAsync(Case @case)
    {
        await _context.AddRangeAsync(@case);
        return @case;
    }
   
}
