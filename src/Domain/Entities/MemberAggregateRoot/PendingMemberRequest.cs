using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.MemberAggregateRoot;

public class PendingMemberRequest(IReadOnlyList<MemberRequest> requests) : BaseEntity
{
    private readonly List<MemberRequest> _requests = new(requests);

    public IReadOnlyList<MemberRequest> Requests
    {
        get => _requests.AsReadOnly();
        private set => _requests.AddRange(value);
    }
}