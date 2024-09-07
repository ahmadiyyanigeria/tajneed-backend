using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Position(string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
}