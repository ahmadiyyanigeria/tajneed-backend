using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.JamaatAggregateRoot;
using Domain.Enums;

namespace Domain.Entities.MemberAggregateRoot;

public class MemberRequest(string surname, string firstName, string auxiliaryBodyId, string middleName, DateTime dob, string email, string phoneNo, string jamaatId, Sex sex, MaritalStatus maritalStatus, string address, Status status, EmploymentStatus employmentStatus, string createdBy) : BaseEntity(createdBy)
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
    public Status Status { get; private set; } = status;
    public EmploymentStatus EmploymentStatus { get; private set; } = employmentStatus;
}