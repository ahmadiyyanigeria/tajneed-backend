using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class Member
(
    string nationalityId, string aimsNo, string membershipStatusId, string nextOfKinName,
    bool isBornMember, string chandaNo, string jamaatId, MembershipInfo membershipInfo, string? wasiyatNo = null, string? spouseNo = null, string? recordFlag = null, string? fatherNo = null, string? childrenNos = null, string? occupation = null, string? nextOfKinPhoneNo = null, string? nextOfKinAddress = null, DateTime? biatDate = null
) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ChandaNo { get; private set; } = chandaNo;
    public string? WasiyatNo { get; private set; } = wasiyatNo;
    public string? SpouseNo { get; set; } = spouseNo;
    public string? FatherNo { get; set; } = fatherNo;
    public string? ChildrenNos { get; set; } = childrenNos;
    public string? AimsNo { get; set; } = aimsNo;
    public string? RecordFlag { get; set; } = recordFlag;
    public string JamaatId { get; private set; } = jamaatId;
    public Jamaat Jamaat { get; private set; } = default!;
    public MembershipInfo MembershipInfo { get; private set; } = membershipInfo;
    public string MembershipStatusId { get; set; } = membershipStatusId;
    public MembershipStatus MembershipStatus { get; private set; } = default!;
    public string? NextOfKinPhoneNo { get; set; } = nextOfKinPhoneNo;
    public string NextOfKinName { get; set; } = nextOfKinName;
    public string? NextOfKinAddress { get; set; } = nextOfKinAddress;
    public bool IsBornMember { get; set; } = isBornMember;
    public string? Occupation { get; set; } = occupation;
    public DateTime? BiatDate { get; set; } = biatDate;
    public string NationalityId { get; set; } = nationalityId;
    public Nationality Nationality { get; private set; } = default!;


}