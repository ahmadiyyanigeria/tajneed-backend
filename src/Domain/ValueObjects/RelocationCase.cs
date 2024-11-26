using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Domain.ValueObjects;

public class RelocationCase(string oldJamaatId, string newJamaatId, string? notes = null)
{
    public string OldJamaatId { get; private set; } = oldJamaatId;
    public Jamaat OldJamaat { get; private set; } = default!;
    public string NewJamaatId { get; private set; } = newJamaatId;
    public Jamaat NewJamaat { get; private set; } = default!;
    public string? Notes { get; private set; } = notes;
}