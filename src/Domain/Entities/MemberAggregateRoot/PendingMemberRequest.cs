using Domain.Entities.AuditTrailAggregateRoot;
using System.ComponentModel;

namespace Domain.Entities.MemberAggregateRoot;

public class PendingMemberRequest(IReadOnlyList<MemberRequest> requests) : BaseEntity, IEntity
{
    private readonly List<MemberRequest> _requests = new(requests);
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public IReadOnlyList<MemberRequest> Requests
    {
        get => _requests.AsReadOnly();
        private set => _requests.AddRange(value);
    }
}