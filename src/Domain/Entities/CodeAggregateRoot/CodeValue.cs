namespace Domain.Entities;

public class CodeValue(string codeId, string @value, string createdBy, string? description = null) : BaseEntity(createdBy)
{
    public string CodeId { get; private set; } = codeId;
    public string Value { get; private set; } = @value;
    public string? Description { get; private set; } = description;

}