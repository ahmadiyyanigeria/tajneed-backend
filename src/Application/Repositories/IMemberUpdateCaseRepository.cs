using TajneedApi.Domain.Entities.CaseAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface IMemberUpdateCaseRepository
{
    Task<MemberUpdateCase> CreateCaseAsync(MemberUpdateCase @case);
    Task<IList<MemberUpdateCase>> GetMemberUpdateCasesByIdsAsync(IList<string> ids, int approvalLevel);

}
