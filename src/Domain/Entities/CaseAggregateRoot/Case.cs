using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Domain.Entities.CaseAggregateRoot;

public class Case(string memberId, Status status, string referenceCode, BiodataUpdateCase? biodataUpdateCase, DuplicateAccountCase? duplicateAccountCase, RelocationCase? relocationCase) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReferenceCode { get; private set; } = referenceCode;
    public CaseType CaseType { get; private set; } = default!;
    public string MemberId { get; private set; } = memberId;
    public Member Member { get; private set; } = default!;
    public Status Status { get; private set; } = status;
    private readonly List<ApprovalMemberUpdateCaseRequestHistory> _approvalHistories = new();
    public BiodataUpdateCase? BiodataUpdateCase { get; private set; } = biodataUpdateCase;
    public RelocationCase? RelocationCase { get; private set; } = relocationCase;
    public DuplicateAccountCase? DuplicateAccountCase { get; private set; } = duplicateAccountCase;
    public IReadOnlyList<ApprovalMemberUpdateCaseRequestHistory> ApprovalHistories => _approvalHistories.AsReadOnly();
    public DisapprovalMemberUpdateCaseRequestHistory? DisApprovalHistory { get; private set; }
    public void AddApprovalHistory(string approvedById, string approvedByRole, string approvedByName)
    {
        if (string.IsNullOrWhiteSpace(approvedById) || string.IsNullOrWhiteSpace(approvedByRole) || string.IsNullOrWhiteSpace(approvedByName))
        {
            var approvalHistory = new ApprovalMemberUpdateCaseRequestHistory(approvedById,approvedByRole,approvedByName);
            _approvalHistories.Add(approvalHistory);
        }

    }
    public void AddDisapprovalHistory(string disapprovedById, string disapprovedByRole, string disapprovedByName)
    {
        if (string.IsNullOrWhiteSpace(disapprovedById) || string.IsNullOrWhiteSpace(disapprovedByRole) || string.IsNullOrWhiteSpace(disapprovedByName))
        {
            DisApprovalHistory = new DisapprovalMemberUpdateCaseRequestHistory(disapprovedById,disapprovedByRole,disapprovedByName);
        }
    }
    public void UpdateStatus(Status status)
    {
        Status = status;
    }

}