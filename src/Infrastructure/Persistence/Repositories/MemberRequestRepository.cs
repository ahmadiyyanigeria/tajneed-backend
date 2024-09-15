using Microsoft.EntityFrameworkCore;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Infrastructure.Extensions;
using TajneedApi.Infrastructure.Persistence.Helpers;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class MemberRequestRepository(ApplicationDbContext context) : IMemberRequestRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PendingMemberRequest> CreateMemberRequestAsync(PendingMemberRequest memberRequest)
    {
        await _context.AddAsync(memberRequest);
        return memberRequest;
    }
    public async Task<PaginatedList<PendingMemberRequest>> GetMemberRequestsAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null)
    {
        var query = _context.PendingMemberRequests.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(circuitId))
        {
            query = query.Where(x => x.Jamaat.CircuitId.Equals(circuitId));
        }
        else if (!string.IsNullOrWhiteSpace(jamaatId))
        {
            query = query.Where(x => x.JamaatId.Equals(jamaatId));
        }

        if (!string.IsNullOrEmpty(pageRequest?.Keyword))
        {
            var helper = new EntitySearchHelper<PendingMemberRequest>(_context);
            query = helper.SearchEntity(pageRequest.Keyword);
        }

        query = query.OrderByDescending(r => r.LastModifiedOn);
        return await query.ToPaginatedList(pageRequest.Page, pageRequest.PageSize, pageRequest.UsePaging);
    }

    public async Task<PendingMemberRequest?> GetMemberRequestAsync(string id) => await _context.PendingMemberRequests
                        .Where(x => x.Id.Equals(id))
                        .FirstOrDefaultAsync();
}
