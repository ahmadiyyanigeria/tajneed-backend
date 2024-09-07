using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.MemberAggregateRoot;

public class MembershipStatus(string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
}