namespace TajneedApi.Application.Queries;
public class GetMember
{
    public record GetMemberQuery : IRequest<IResult<MemberResponse>>
    {
        public string Id { get; set; } = default!;
    }

    public class Handler(IMemberRepository memberRepository) : IRequestHandler<GetMemberQuery, IResult<MemberResponse>>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;

        public async Task<IResult<MemberResponse>> Handle(GetMemberQuery request, CancellationToken cancellationToken)
        {
            var memberRequest = await _memberRepository.GetMemberAsync(request.Id);
            if (memberRequest is null)
                return await Result<MemberResponse>.FailAsync("Member request not found");

            //TODO: investigate why the response does not contian LastModifiedBy 
            var response = memberRequest.Adapt<MemberResponse>();
            return await Result<MemberResponse>.SuccessAsync(response);
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
        DateTime? BiatDate,
        MembershipRequestResponse MembershipRequest
    );
    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
