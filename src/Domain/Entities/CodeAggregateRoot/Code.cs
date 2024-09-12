using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.CodeAggregateRoot;

public class Code(string name, string? description = null) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;

    public string? Description { get; private set; } = description;

}