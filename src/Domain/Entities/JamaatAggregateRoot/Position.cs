using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Position(string name) : BaseEntity
{
    public string Name { get; private set; } = name;
}