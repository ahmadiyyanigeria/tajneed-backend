namespace Domain.Entities;
public class AuditTrail(string userId, string activityId, string details, DateTime dateOccurred)
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string ActivityId { get; private set; } = activityId;
    public string UserId { get; private set; } = userId;
    public string Details { get; private set; } = details;
    public DateTime DateOccurred { get; private set; } = dateOccurred;
}