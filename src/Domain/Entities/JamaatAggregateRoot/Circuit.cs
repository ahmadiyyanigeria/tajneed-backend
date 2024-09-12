using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.JamaatAggregateRoot;

public class Circuit(string circuitCode, string circuitName) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CircuitName { get; private set; } = circuitName;
    public string CircuitCode { get; private set; } = circuitCode;

}