using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.JamaatAggregateRoot;

public class Jamaat(string jamaatName, string jamaatCode, string circuitId) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string JamaatName { get; private set; } = jamaatName;
    public string JamaatCode { get; private set; } = jamaatCode;
    public string CircuitId { get; private set; } = circuitId;
    public Circuit Circuit { get; private set; } = default!;


}