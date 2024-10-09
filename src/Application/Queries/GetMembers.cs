using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Application.Queries;
public class GetMembers
{
    public record GetMembersQuery : PageRequest, IRequest<PaginatedList<MemberResponse>>
    {
        public string? JamaatId { get; set; } = default;
        public string? CircuitId { get; set; } = default;
        public RequestStatus? RequestStatus { get; set; } = default;
    }

    public class Handler(IMemberRepository memberRepository) : IRequestHandler<GetMembersQuery, PaginatedList<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;

        public async Task<PaginatedList<MemberResponse>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
        {
            var memberRequests = await _memberRepository.GetMembersAsync(request, request.JamaatId, request.CircuitId);
            return memberRequests.Adapt<PaginatedList<MemberResponse>>();
        }
    }

    public record MemberResponse(
        string ChandaNo,
        string MembershipRequestId,
        string? AimsNo,
        string? NextOfKinName,
        string? WasiyatNo,
        string? SpouseNo,
        string? RecordFlag,
        string? FatherNo,
        string? ChildrenNos,
        string? Occupation,
        string? NextOfKinPhoneNo,
        string? NextOfKinAddress,
        DateTime? BiatDate    
    );

}
