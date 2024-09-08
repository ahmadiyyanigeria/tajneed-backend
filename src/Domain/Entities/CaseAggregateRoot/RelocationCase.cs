using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.JamaatAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class RelocationCase(string caseId, string oldJamaatId, string newJamaatId, string? notes = null) : BaseEntity, IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CaseId { get; private set; } = caseId;
    public Case Case { get; private set; } = default!;
    public string OldJamaatId { get; private set; } = oldJamaatId;
    public Jamaat OldJamaat { get; private set; } = default!;
    public string NewJamaatId { get; private set; } = newJamaatId;
    public Jamaat NewJamaat { get; private set; } = default!;
    public string? Notes { get; private set; } = notes;
}