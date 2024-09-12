using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.Enums;

namespace TajneedApi.Domain.ValueObjects;

public class MembershipInfo(string surname, string firstName, string auxiliaryBodyId, string middleName, DateTime dob, string email, string phoneNo, string jamaatId, Sex sex, MaritalStatus maritalStatus, string address, EmploymentStatus employmentStatus)
{
    public string Surname { get; private set; } = surname;
    public string FirstName { get; private set; } = firstName;
    public string AuxiliaryBodyId { get; private set; } = auxiliaryBodyId;
    public AuxiliaryBody AuxiliaryBody { get; private set; } = default!;
    public string MiddleName { get; private set; } = middleName;
    public DateTime Dob { get; private set; } = dob;
    public string Email { get; private set; } = email;
    public string PhoneNo { get; private set; } = phoneNo;
    public string JamaatId { get; private set; } = jamaatId;
    public Jamaat Jamaat { get; private set; } = default!;
    public Sex Sex { get; private set; } = sex;
    public MaritalStatus MaritalStatus { get; private set; } = maritalStatus;
    public string Address { get; private set; } = address;
    public Status Status { get; private set; } = Status.Active;
    public EmploymentStatus EmploymentStatus { get; private set; } = employmentStatus;

}