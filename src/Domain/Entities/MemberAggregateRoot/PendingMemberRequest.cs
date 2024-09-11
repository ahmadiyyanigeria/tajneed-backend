using Domain.Entities.AuditTrailAggregateRoot;
using System.ComponentModel;

namespace Domain.Entities.MemberAggregateRoot;

public class PendingMemberRequest(IReadOnlyList<MembershipInfo> requests) : BaseEntity
{
    private readonly List<MembershipInfo> _requests = new(requests);
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public IReadOnlyList<MembershipInfo> Requests
    {
        get => _requests.AsReadOnly();
        private set => _requests.AddRange(value);
    }
}