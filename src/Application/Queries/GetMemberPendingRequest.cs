using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Application.Queries;
public class GetPendingMemberRequest
{
    public record GetMemberRequestQuery : IRequest<IResult<MemberRequestResponse>>
    {
        public string Id { get; set; } = default!;
    }

    public class Handler(IMemberRequestRepository memberRequestRepository) : IRequestHandler<GetMemberRequestQuery, IResult<MemberRequestResponse>>
    {
        private readonly IMemberRequestRepository _memberRequestRepository = memberRequestRepository;

        public async Task<IResult<MemberRequestResponse>> Handle(GetMemberRequestQuery request, CancellationToken cancellationToken)
        {
            var memberRequest = await _memberRequestRepository.GetMemberRequestAsync(request.Id);
            if (memberRequest is null)
                return await Result<MemberRequestResponse>.FailAsync("Member request not found");

            //TODO: investigate why the response does not contian LastModifiedBy 
            var response = memberRequest.Adapt<MemberRequestResponse>();
            return await Result<MemberRequestResponse>.SuccessAsync(response);
        }
    }

    public record MemberRequestResponse(string Id, IReadOnlyList<MembershipInfoResponse> Requests, DateTime CreatedOn, string CreatedBy, string LastModifiedBy, DateTime LastModifiedOn);

    public record MembershipInfoResponse(string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, DateTime Dob, string Email, string PhoneNo, string JamaatId, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
