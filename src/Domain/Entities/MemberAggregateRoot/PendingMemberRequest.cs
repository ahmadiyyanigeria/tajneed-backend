using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class PendingMemberRequest(string jamaatId, IReadOnlyList<MembershipInfo> requests) : BaseEntity
{
    private readonly List<MembershipInfo> _requests = new(requests);
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JamaatId { get; private set; } = jamaatId;
    public Jamaat Jamaat { get; private set; } = default!;

    public IReadOnlyList<MembershipInfo> Requests
    {
        get => _requests.AsReadOnly();
        private set => _requests.AddRange(value);
    }
}