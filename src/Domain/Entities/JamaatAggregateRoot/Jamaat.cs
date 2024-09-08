using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Jamaat(string jamaatName, string jamaatCode, string circuitId) : BaseEntity, IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JamaatName { get; private set; } = jamaatName;
    public string JamaatCode { get; private set; } = jamaatCode;
    public string CircuitId { get; private set; } = circuitId;
    public Circuit Circuit { get; private set; } = default!;


}