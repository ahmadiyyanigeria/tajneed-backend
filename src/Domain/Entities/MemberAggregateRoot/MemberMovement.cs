using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.JamaatAggregateRoot;

namespace Domain.Entities.MemberAggregateRoot;

public class MemberMovement(string memberId, string fromJamaatId, string toJamaatId, string createdBy) : BaseEntity(createdBy)
{
    public string MemberId { get; private set; } = memberId;
    public string FromJamaatId { get; private set; } = fromJamaatId;
    public Jamaat FromJamaat { get; private set; } = default!;
    public string ToJamaatId { get; private set; } = toJamaatId;
    public Jamaat ToJamaat { get; private set; } = default!;
    public DateTime MovementDate { get; private set; } = DateTime.UtcNow;

}