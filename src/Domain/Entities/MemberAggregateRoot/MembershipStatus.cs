using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.MemberAggregateRoot;

public class MembershipStatus(string name) : BaseEntity
{
    public string Name { get; private set; } = name;
}