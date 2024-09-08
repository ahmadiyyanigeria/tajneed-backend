namespace Domain.Entities.AuditTrailAggregateRoot;

public class BaseEntity
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool IsDeleted { get; set; }
    public string? LastModifiedBy { get; set; }
}

public interface IEntity
{
    string Id { get; set; }
}