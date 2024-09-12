namespace TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

public class BaseEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = default!;
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public string? LastModifiedBy { get; set; }
}


