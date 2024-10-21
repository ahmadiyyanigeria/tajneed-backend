namespace TajneedApi.Domain.ValueObjects;

public class ApprovalMemberRequestHistory(string ApprovedById, string ApprovedByRole, string ApprovedByName)
{
    public string ApprovedById { get; private set; } = ApprovedById;
    public string ApprovedByRole { get; private set; } = ApprovedByRole;
    public string ApprovedByName { get; private set; } = ApprovedByName;
    public DateTime ApprovalDate { get; private set; } = DateTime.UtcNow;
}