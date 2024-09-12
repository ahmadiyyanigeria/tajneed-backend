using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class MembershipStatus(string name) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
}