namespace Domain.Entities.AuditTrailAggregateRoot;

public class BaseEntity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public string? LastModifiedBy { get; set; }

}