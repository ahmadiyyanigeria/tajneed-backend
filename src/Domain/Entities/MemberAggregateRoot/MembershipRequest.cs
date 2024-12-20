using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Domain.Entities.MemberAggregateRoot;

public class MembershipRequest(string surname, string firstName, string nationalityId, bool isBornMember, string auxiliaryBodyId, string middleName, DateTime dob, string email, string phoneNo, Sex sex, MaritalStatus maritalStatus, string address, EmploymentStatus employmentStatus, string occupation, string batchRequestId, string jamaatId, DateTime? biatDate) : BaseEntity
{
    public string JamaatId { get; private set; } = jamaatId;
    public Jamaat Jamaat { get; private set; } = default!;
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string BatchRequestId { get; private set; } = batchRequestId;
    private readonly List<ApprovalMemberRequestHistory> _approvalHistories = new();
    public string Surname { get; private set; } = surname;
    public string FirstName { get; private set; } = firstName;
    public string AuxiliaryBodyId { get; private set; } = auxiliaryBodyId;
    public AuxiliaryBody AuxiliaryBody { get; private set; } = default!;
    public string MiddleName { get; private set; } = middleName;
    public DateTime Dob { get; private set; } = dob;
    public string Email { get; private set; } = email;
    public string PhoneNo { get; private set; } = phoneNo;
    public Sex Sex { get; private set; } = sex;
    public MaritalStatus MaritalStatus { get; private set; } = maritalStatus;
    public string Address { get; private set; } = address;
    public RequestStatus RequestStatus { get; private set; } = RequestStatus.Pending;
    public EmploymentStatus EmploymentStatus { get; private set; } = employmentStatus;
    public bool IsBornMember { get; private set; } = isBornMember;
    public string Occupation { get; private set; } = occupation;
    public DateTime? BiatDate { get; private set; } = biatDate;
    public string NationalityId { get; private set; } = nationalityId;
    public Nationality Nationality { get; private set; } = default!;
    public Member? Member { get; private set; }
    public IReadOnlyList<ApprovalMemberRequestHistory> ApprovalHistories => _approvalHistories.AsReadOnly();
    public DisapprovalMemberRequestHistory? DisApprovalHistory { get; private set; }
    public void AddApprovalHistory(string approvedById, string approvedByRole, string approvedByName)
    {
        if (string.IsNullOrWhiteSpace(approvedById) || string.IsNullOrWhiteSpace(approvedByRole) || string.IsNullOrWhiteSpace(approvedByName))
        {
            var approvalHistory = new ApprovalMemberRequestHistory(approvedById, approvedByRole, approvedByName);
            _approvalHistories.Add(approvalHistory);
        }

    }
    public void AddDisapprovalHistory(string disapprovedById, string disapprovedByRole, string disapprovedByName)
    {
        if (string.IsNullOrWhiteSpace(disapprovedById) || string.IsNullOrWhiteSpace(disapprovedByRole) || string.IsNullOrWhiteSpace(disapprovedByName))
        {
            DisApprovalHistory = new DisapprovalMemberRequestHistory(disapprovedById, disapprovedByRole, disapprovedByName);
        }
    }
    public void UpdateRequestStatus(RequestStatus requestStatus)
    {
        RequestStatus = requestStatus;
    }

}