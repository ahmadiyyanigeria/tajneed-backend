using Domain.Entities.MemberAggregateRoot;

namespace Application.Repositories;

public interface IMemberRequestRepository
{
    Task<MemberRequest> CreateMemberRequestAsync(MemberRequest memberRequest);
}
