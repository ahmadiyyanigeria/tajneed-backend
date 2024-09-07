using Domain.Entities.JamaatAggregateRoot;
using Domain.Enums;

namespace Domain.Entities.MemberAggregateRoot;

public class Member
(
    string nationalityId, string aimsNo, string membershipStatusId, string nextOfKinName,
    bool isBornMember, string chandaNo, string surname, string firstName, string auxiliaryBodyId, string middleName,
    DateTime dob, string email, string phoneNo, string jamaatId, Sex sex, MaritalStatus maritalStatus, string address,
    Status status, EmploymentStatus employmentStatus, string createdBy, string? wasiyatNo = null, string? spouseNo = null,
    string? recordFlag = null, string? fatherNo = null, string? childrenNos = null, string? occupation = null,
    string? nextOfKinPhoneNo = null, string? nextOfKinAddress = null, DateTime? biatDate = null
)
: MemberRequest
(
    surname, firstName, auxiliaryBodyId, middleName, dob, email,
    phoneNo, jamaatId, sex, maritalStatus, address, status, employmentStatus, createdBy
)
{
    public string ChandaNo { get; private set; } = chandaNo;
    public string? WasiyatNo { get; private set; } = wasiyatNo;
    public string? SpouseNo { get; set; } = spouseNo;
    public string? FatherNo { get; set; } = fatherNo;
    public string? ChildrenNos { get; set; } = childrenNos;
    public string? AimsNo { get; set; } = aimsNo;
    public string? RecordFlag { get; set; } = recordFlag;
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