using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Circuit(string circuitCode, string circuitName) : BaseEntity
{
    public string CircuitName { get; private set; } = circuitName;
    public string CircuitCode { get; private set; } = circuitCode;

}