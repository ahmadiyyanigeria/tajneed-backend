using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;

namespace TajneedApi.Domain.Entities.HouseHoldAggregateRoot;
public class HouseHoldMember(string houseHoldId, string memberId, string positionId) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string HouseHoldId { get; private set; } = houseHoldId;
    public HouseHold HouseHold { get; private set; } = default!;
    public string MemberId { get; private set; } = memberId;
    public Member Member { get; private set; } = default!;
    public string PositionId { get; private set; } = positionId;
    public Position Position { get; private set; } = default!;


}