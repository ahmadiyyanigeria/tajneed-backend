namespace TajneedApi.Application.Queries;
public class GetMembershipRequest
{
    public record GetMemberRequestQuery : IRequest<IResult<MembershipRequestResponse>>
    {
        public string Id { get; set; } = default!;
    }

    public class Handler(IMembershipRequestRepository memberRequestRepository) : IRequestHandler<GetMemberRequestQuery, IResult<MembershipRequestResponse>>
    {
        private readonly IMembershipRequestRepository _memberRequestRepository = memberRequestRepository;

        public async Task<IResult<MembershipRequestResponse>> Handle(GetMemberRequestQuery request, CancellationToken cancellationToken)
        {
            var memberRequest = await _memberRequestRepository.GetMemberRequestAsync(request.Id);
            if (memberRequest is null)
                return await Result<MembershipRequestResponse>.FailAsync("Member request not found");

            //TODO: investigate why the response does not contian LastModifiedBy 
            var response = memberRequest.Adapt<MembershipRequestResponse>();
            return await Result<MembershipRequestResponse>.SuccessAsync(response);
        }
    }


    public record MembershipRequestResponse(string Id, string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, string JamaatId, string BatchRequestId, DateTime Dob, string Email, string PhoneNo, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
