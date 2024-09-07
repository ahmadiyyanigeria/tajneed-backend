using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CodeAggregateRoot;

public class Code(string name, string createdBy, string? description = null) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;

    public string? Description { get; private set; } = description;

}