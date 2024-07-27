using Domain.Enums;

namespace Domain.Entities;

public class MemberRequest(string chandaNo, string surname, string firstName, string auxillaryBodyId, string middleName, DateTime dob, string email, string phoneNo, string jamaatId, Sex sex, MaritalStatus maritalStatus, string address, Status status, EmploymentStatus employmentStatus, string createdBy, string? wasiyatNo = null) : BaseEntity(createdBy)
{
    public string ChandaNo { get; private set; } = chandaNo;
    public string? WasiyatNo { get; private set; } = wasiyatNo;
    public string Surname { get; private set; } = surname;
    public string FirstName { get; private set; } = firstName;
    public string AuxillaryBodyId { get; private set; } = auxillaryBodyId;
    public string MiddleName { get; private set; } = middleName;
    public DateTime Dob { get; private set; } = dob;
    public string Email { get; private set; } = email;
    public string PhoneNo { get; private set; } = phoneNo;
    public string JamaatId { get; private set; } = jamaatId;
    public Sex Sex { get; private set; } = sex;
    public MaritalStatus MaritalStatus { get; private set; } = maritalStatus;
    public string Address { get; private set; } = address;
    public Status Status { get; private set; } = status;
    public EmploymentStatus EmploymentStatus { get; private set; } = employmentStatus;
}