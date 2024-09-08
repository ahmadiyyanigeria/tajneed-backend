using Domain.Entities.MemberAggregateRoot;

namespace Application.Repositories;

public interface IMemberRequestRepository
{
    Task<PendingMemberRequest> CreateMemberRequestAsync(PendingMemberRequest memberRequest);
}
