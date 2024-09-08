using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Entities.MemberAggregateRoot;

namespace Domain.Entities.CaseAggregateRoot;

public class Case(string caseTypeId, string memberId, string status, string referenceCode, string details) : BaseEntity
{
    public string ReferenceCode { get; private set; } = referenceCode;
    public string CaseTypeId { get; private set; } = caseTypeId;
    public CaseType CaseType { get; private set; } = default!;
    public string MemberId { get; private set; } = memberId;
    public Member Member { get; private set; } = default!;
    public string Status { get; private set; } = status;
    public string Details { get; private set; } = details;
}