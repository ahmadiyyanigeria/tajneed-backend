namespace TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

public class BaseEntity : ISoftDelete
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = default!;
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public string? LastModifiedBy { get; set; }
}

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}