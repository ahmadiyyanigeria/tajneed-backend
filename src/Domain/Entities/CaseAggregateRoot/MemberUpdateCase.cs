using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Domain.Entities.CaseAggregateRoot;

public class MemberUpdateCase(string memberId, BiodataUpdateCase? biodataUpdateCase, DuplicateAccountCase? duplicateAccountCase, RelocationCase? relocationCase) : BaseEntity
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();
    public string MemberId { get; private set; } = memberId;
    public Member Member { get;  set; } = default!;
    public RequestStatus RequestStatus { get; private set; } = RequestStatus.Pending;
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
    public void UpdateStatus(RequestStatus status)
    {
        RequestStatus = status;
    }

}