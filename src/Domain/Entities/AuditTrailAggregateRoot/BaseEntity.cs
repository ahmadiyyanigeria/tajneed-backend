namespace Domain.Entities;

public class BaseEntity(string createdBy)
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = createdBy;
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; private set; } = default;
    public string? LastModifiedBy { get; private set; }

}