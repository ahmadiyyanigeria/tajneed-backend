using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class CaseType(string code, string? description = null) : BaseEntity, IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Code { get; private set; } = code;
    public string? Description { get; private set; } = description;
}