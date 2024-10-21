using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class Member
(string chandaNo, string membershipRequestId, string? aimsNo = null, string? nextOfKinName = null, string? wasiyatNo = null, string? spouseNo = null, string? recordFlag = null, string? fatherNo = null, string? childrenNos = null, string? nextOfKinPhoneNo = null, string? nextOfKinAddress = null) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ChandaNo { get; private set; } = chandaNo;
    public string? WasiyatNo { get; private set; } = wasiyatNo;
    public string? SpouseNo { get; set; } = spouseNo;
    public string? FatherNo { get; set; } = fatherNo;
    public string? ChildrenNos { get; set; } = childrenNos;
    public string? AimsNo { get; set; } = aimsNo;
    public string? RecordFlag { get; set; } = recordFlag;
    public string MembershipRequestId { get; set; } = membershipRequestId;
    public MembershipRequest MembershipRequest { get; set; } = default!;
    public MembershipStatus MembershipStatus { get; private set; } = MembershipStatus.Active;
    public string? NextOfKinPhoneNo { get; set; } = nextOfKinPhoneNo;
    public string? NextOfKinName { get; set; } = nextOfKinName;
    public string? NextOfKinAddress { get; set; } = nextOfKinAddress;

}