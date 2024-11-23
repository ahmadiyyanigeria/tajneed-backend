using TajneedApi.Application.Repositories;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Infrastructure.Extensions;
using TajneedApi.Infrastructure.Persistence.Helpers;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class MembershipRequestRepository(ApplicationDbContext context) : IMembershipRequestRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IList<MembershipRequest>> CreateMemberRequestAsync(IList<MembershipRequest> memberRequest)
    {
        await _context.AddRangeAsync(memberRequest);
        return memberRequest;
    }


    public async Task<PaginatedList<IGrouping<string, MembershipRequest>>> GetMemberRequestsAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null, RequestStatus? requestStatus = null)
    {
        var query = _context.MembershipRequests.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(circuitId))
        {
            query = query.Where(x => x.Jamaat.CircuitId == circuitId);
        }
        if (!string.IsNullOrWhiteSpace(jamaatId))
        {
            query = query.Where(x => x.JamaatId == jamaatId);
        }
        if (requestStatus != null)
        {
            query = query.Where(x => x.RequestStatus == requestStatus);
        }

        if (!string.IsNullOrEmpty(pageRequest?.Keyword))
        {
            query = new EntitySearchHelper<MembershipRequest>(_context)
                .SearchEntity(pageRequest.Keyword);
        }

        var groupedQuery = query
            .OrderByDescending(r => r.LastModifiedOn)
            .GroupBy(r => r.BatchRequestId);

        return await groupedQuery.ToPaginatedList(pageRequest.Page, pageRequest.PageSize, pageRequest.UsePaging);
    }



    public async Task<MembershipRequest?> GetMemberRequestAsync(string id) => await _context.MembershipRequests
                        .Where(x => x.Id.Equals(id))
                        .FirstOrDefaultAsync();

    public async Task<PaginatedList<IGrouping<string, MembershipRequest>>> GetToBeApprovedMembershipRequestsAsync(PageRequest pageRequest, int approvalLevel, string? jamaatId = null, string? circuitId = null)
    {
        var query = _context.MembershipRequests.Where(x => x.RequestStatus == RequestStatus.Pending && x.ApprovalHistories.Count == approvalLevel).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(circuitId))
        {
            query = query.Where(x => x.Jamaat.CircuitId == circuitId);
        }
        if (!string.IsNullOrWhiteSpace(jamaatId))
        {
            query = query.Where(x => x.JamaatId == jamaatId);
        }

        if (!string.IsNullOrEmpty(pageRequest?.Keyword))
        {
            query = new EntitySearchHelper<MembershipRequest>(_context)
                .SearchEntity(pageRequest.Keyword);
        }

        var groupedQuery = query
            .OrderByDescending(r => r.LastModifiedOn)
            .GroupBy(r => r.BatchRequestId);

        return await groupedQuery.ToPaginatedList(pageRequest.Page, pageRequest.PageSize, pageRequest.UsePaging);
    }

    public async Task<IList<MembershipRequest>> GetMemberRequestsByIdsAsync(IList<string> ids, int approvalLevel)
    {
        return await _context.MembershipRequests.Where(a => ids.Contains(a.Id) && a.ApprovalHistories.Count == approvalLevel).ToListAsync();
    }

    public Task<IList<MembershipRequest>> RecommendPossibleDuplicateMemberRequestAsync(IList<MembershipRequest> memberRequest)
    {
        throw new NotImplementedException();
    }

}
