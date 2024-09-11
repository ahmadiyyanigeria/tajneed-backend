using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CodeAggregateRoot;

public class CodeValue(string codeId, string @value, string? description = null) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CodeId { get; private set; } = codeId;
    public Code Code { get; private set; } = default!;

    public string Value { get; private set; } = @value;
    public string? Description { get; private set; } = description;

}