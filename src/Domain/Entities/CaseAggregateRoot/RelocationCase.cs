namespace Domain.Entities;

public class RelocationCase(string caseId, string oldJamaatId, string newJamaatId, string createdBy, string? notes = null) : BaseEntity(createdBy)
{
    public string CaseId { get; private set; } = caseId;
    public Case Case { get; private set; } = default!;
    public string OldJamaatId { get; private set; } = oldJamaatId;
    public Jamaat OldJamaat { get; private set; } = default!;
    public string NewJamaatId { get; private set; } = newJamaatId;
    public Jamaat NewJamaat { get; private set; } = default!;
    public string? Notes { get; private set; } = notes;
}