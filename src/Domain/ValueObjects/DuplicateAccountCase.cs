using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.ValueObjects;

public class DuplicateAccountCase(string primaryAccount, string otherAccounts, string? notes = null)
{
    public string PrimaryAccount { get; private set; } = primaryAccount;
    public string OtherAccounts { get; private set; } = otherAccounts;
    public string? Notes { get; private set; } = notes;
}