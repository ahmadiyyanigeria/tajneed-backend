namespace Domain.Entities;
public class HouseHoldMember(string houseHoldId, string memberId, string positionId, string createdBy) : BaseEntity(createdBy)
{
    public string HouseHoldId { get; private set; } = houseHoldId;
    public HouseHold HouseHold { get; private set; } = default!;
    public string MemberId { get; private set; } = memberId;
    public Member Member { get; private set; } = default!;
    public string PositionId { get; private set; } = positionId;
    public Position Position { get; private set; } = default!;
 

}