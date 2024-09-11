using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Position(string name) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
}