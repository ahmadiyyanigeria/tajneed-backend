namespace TajneedApi.Domain.ValueObjects;

public class DisapprovalMemberRequestHistory(string disapprovedById, string disapprovedByRole, string disapprovedByName)
{
    public string DisapprovedById { get; private set; } = disapprovedById;
    public string DisapprovedByRole { get; private set; } = disapprovedByRole;
    public string DisapprovedByName { get; private set; } = disapprovedByName;
    public DateTime DisapprovalDate { get; private set; } = DateTime.UtcNow;
}