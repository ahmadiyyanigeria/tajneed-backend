using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class DuplicateAccountCase(string primaryAccount, string otherAccounts, string? notes = null) : BaseEntity
{
    public string PrimaryAccount { get; private set; } = primaryAccount;
    public string OtherAccounts { get; private set; } = otherAccounts;
    public string? Notes { get; private set; } = notes;
}