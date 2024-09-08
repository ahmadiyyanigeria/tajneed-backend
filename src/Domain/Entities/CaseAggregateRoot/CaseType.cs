using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class CaseType(string code, string? description = null) : BaseEntity
{
    public string Code { get; private set; } = code;
    public string? Description { get; private set; } = description;
}