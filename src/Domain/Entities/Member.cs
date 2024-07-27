using Domain.Enums;

namespace Domain.Entities;

public class Member
(
    string nationalityId, string aIMSNo, string membershipStatusId, string nextOfKinName, 
    bool isBornMember, string chandaNo, string surname, string firstName, string auxillaryBodyId, string middleName, 
    DateTime dob, string email, string phoneNo, string jamaatId, Sex sex, MaritalStatus maritalStatus, string address, 
    Status status, EmploymentStatus employmentStatus, string createdBy, string? wasiyatNo = null, string? spouseNo= null, 
    string? recordFlag = null,string? fatherNo = null, string? childrenNos = null, string? occupation = null, 
    string? nextOfKinPhoneNo = null, string? nextOfKinAddress = null, DateTime? biatDate = null
) 
: MemberRequest
(
    chandaNo, surname, firstName, auxillaryBodyId, middleName, dob, email, 
    phoneNo, jamaatId, sex, maritalStatus, address, status, employmentStatus, createdBy, wasiyatNo
)

{
    public string? SpouseNo { get; set; } = spouseNo;
    public string? FatherNo { get; set; } = fatherNo;
    public string? ChildrenNos { get; set; } = childrenNos;
    public string? AIMSNo { get; set; } = aIMSNo;
    public string? RecordFlag { get; set; } = recordFlag;
    public string MembershipStatusId { get; set; } = membershipStatusId;
    public string? NextOfKinPhoneNo { get; set; } = nextOfKinPhoneNo;
    public string NextOfKinName { get; set; } = nextOfKinName;
    public string? NextOfKinAddress { get; set; } = nextOfKinAddress;
    public bool IsBornMember { get; set; } = isBornMember;
    public string? Occupation { get; set; } = occupation;
    public DateTime? BiatDate { get; set; } = biatDate;
    public string NationalityId { get; set; } = nationalityId;

}