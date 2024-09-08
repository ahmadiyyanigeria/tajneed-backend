using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class DuplicateAccountCase(string caseId, string primaryAccount, string otherAccounts, string? notes = null) : BaseEntity, IEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CaseId { get; private set; } = caseId;
    public Case Case { get; private set; } = default!;
    public string PrimaryAccount { get; private set; } = primaryAccount;
    public string OtherAccounts { get; private set; } = otherAccounts;
    public string? Notes { get; private set; } = notes;
}