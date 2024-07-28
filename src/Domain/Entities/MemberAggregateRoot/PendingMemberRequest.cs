namespace Domain.Entities;
public class PendingMemberRequest(IReadOnlyList<MemberRequest> requests, string createdBy) : BaseEntity(createdBy)
{
    
    private readonly List<MemberRequest> _requests = new(requests);
    public IReadOnlyList<MemberRequest> Requests
    {
        get => _requests.AsReadOnly();
        private set => _requests.AddRange(value);
    }


}