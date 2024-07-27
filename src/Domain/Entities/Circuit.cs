namespace Domain.Entities;

public class Circuit(string circuitCode, string circuitName, string createdBy) : BaseEntity(createdBy)
{
    public string CircuitName { get; private set; } = circuitName;
    public string CircuitCode { get; private set; } = circuitCode;

}