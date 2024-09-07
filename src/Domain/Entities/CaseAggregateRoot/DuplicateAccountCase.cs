using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class DuplicateAccountCase(string caseId, string primaryAccount, string otherAccounts, string createdBy, string? notes = null) : BaseEntity(createdBy)
{
    public string CaseId { get; private set; } = caseId;
    public Case Case { get; private set; } = default!;
    public string PrimaryAccount { get; private set; } = primaryAccount;
    public string OtherAccounts { get; private set; } = otherAccounts;
    public string? Notes { get; private set; } = notes;
}