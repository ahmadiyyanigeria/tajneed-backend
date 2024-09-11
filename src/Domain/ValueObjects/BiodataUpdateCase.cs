using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.HouseHoldAggregateRoot;
using Domain.Entities.JamaatAggregateRoot;
using Domain.Entities.MemberAggregateRoot;
using Domain.Enums;

namespace Domain.Entities.CaseAggregateRoot;

public class BiodataUpdateCase(
string surName, string firstName, string middleName, DateTime dob, string address, string email, string phoneNumber,
EmploymentStatus employmentStatus, string jamaatId, MaritalStatus maritalStatus, string membershipStatusId, string title,
bool isBornMember, string nationalityId, string householdMemberId, Sex sex, string maidenName, string? notes = null,
string? spouseNo = null, string? fatherNo = null, string? childrenNos = null, string? biometricId = null, string? nextOfKinPhoneNo = null,
string? occupation = null, DateTime? biatDate = null) : BaseEntity
{
    public string SurName { get; private set; } = surName;
    public string FirstName { get; private set; } = firstName;
    public string MiddleName { get; private set; } = middleName;
    public string Title { get; private set; } = title;
    public DateTime Dob { get; private set; } = dob;
    public Sex Sex { get; private set; } = sex;
    public string Address { get; private set; } = address;
    public string MaidenName { get; private set; } = maidenName;
    public string Email { get; private set; } = email;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public EmploymentStatus EmploymentStatus { get; private set; } = employmentStatus;
    public string JamaatId { get; private set; } = jamaatId;
    public Jamaat Jamaat { get; private set; } = default!;
    public string? Notes { get; private set; } = notes;
    public string? SpouseNo { get; private set; } = spouseNo;
    public string? FatherNo { get; private set; } = fatherNo;
    public string? ChildrenNos { get; private set; } = childrenNos;
    public string? BiometricId { get; private set; } = biometricId;
    public MaritalStatus MaritalStatus { get; private set; } = maritalStatus;
    public string MembershipStatusId { get; private set; } = membershipStatusId;
    public MembershipStatus MembershipStatus { get; private set; } = default!;
    public string? NextOfKinPhoneNo { get; private set; } = nextOfKinPhoneNo;
    public bool IsBornMember { get; private set; } = isBornMember;
    public string? Occupation { get; private set; } = occupation;
    public DateTime? BiatDate { get; private set; } = biatDate;
    public string NationalityId { get; private set; } = nationalityId;
    public Nationality Nationality { get; private set; } = default!;
    public string HouseholdMemberId { get; private set; } = householdMemberId;
    public HouseHoldMember HouseholdMember { get; private set; } = default!;
}