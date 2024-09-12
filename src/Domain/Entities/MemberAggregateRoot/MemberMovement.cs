using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class MemberMovement(string memberId, string fromJamaatId, string toJamaatId) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string MemberId { get; private set; } = memberId;
    public string FromJamaatId { get; private set; } = fromJamaatId;
    public Jamaat FromJamaat { get; private set; } = default!;
    public string ToJamaatId { get; private set; } = toJamaatId;
    public Jamaat ToJamaat { get; private set; } = default!;
    public DateTime MovementDate { get; private set; } = DateTime.UtcNow;

}