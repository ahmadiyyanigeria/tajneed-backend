using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.JamaatAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class RelocationCase(string oldJamaatId, string newJamaatId, string? notes = null) : BaseEntity
{
    public string OldJamaatId { get; private set; } = oldJamaatId;
    public Jamaat OldJamaat { get; private set; } = default!;
    public string NewJamaatId { get; private set; } = newJamaatId;
    public Jamaat NewJamaat { get; private set; } = default!;
    public string? Notes { get; private set; } = notes;
}