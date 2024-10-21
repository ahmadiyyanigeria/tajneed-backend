using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Entities.MemberAggregateRoot;
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
    public BiodataUpdateCase? BiodataUpdateCase { get; private set; } = biodataUpdateCase;
    public RelocationCase? RelocationCase { get; private set; } = relocationCase;
    public DuplicateAccountCase? DuplicateAccountCase { get; private set; } = duplicateAccountCase;

}