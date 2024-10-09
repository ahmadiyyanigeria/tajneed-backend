using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Infrastructure.Extensions;
using TajneedApi.Infrastructure.Persistence.Helpers;

namespace TajneedApi.Infrastructure.Persistence.Repositories
{
    public class MemberRepository(ApplicationDbContext context) : IMemberRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IList<Member>> CreateMemberAsync(IList<Member> members)
        {
            await _context.Members.AddRangeAsync(members);
            return members;
        }

        public async Task<Member?> GetMemberAsync(string id)
        {
            return await _context.Members.Include(a => a.MembershipRequest).Where(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<PaginatedList<Member>> GetMembersAsync(PageRequest pageRequest, string? jamaatId = null, string? circuitId = null)
        {
            var query = _context.Members.Include(a => a.MembershipRequest).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(circuitId))
            {
                query = query.Where(x => x.MembershipRequest.Jamaat.CircuitId.Equals(circuitId));
            }
            else if (!string.IsNullOrWhiteSpace(jamaatId))
            {
                query = query.Where(x => x.MembershipRequest.JamaatId.Equals(jamaatId));
            }

            if (!string.IsNullOrEmpty(pageRequest?.Keyword))
            {
                var helper = new EntitySearchHelper<Member>(_context);
                query = helper.SearchEntity(pageRequest.Keyword);
            }

            query = query.OrderByDescending(r => r.LastModifiedOn);
            return await query.ToPaginatedList(pageRequest.Page, pageRequest.PageSize, pageRequest.UsePaging);
        }

    }
}