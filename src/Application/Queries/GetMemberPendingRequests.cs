
using Mapster;
using MediatR;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.Repositories.Paging;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Domain.ValueObjects;

namespace TajneedApi.Application.Queries;
public class GetMemberPendingRequests
{
    public record GetMemberRequestsQuery : PageRequest, IRequest<PaginatedList<MemberRequestResponse>>
    {
        public string? JamaatId { get; set; } = default;
        //status
    }

    public class Handler : IRequestHandler<GetMemberRequestsQuery, PaginatedList<MemberRequestResponse>>
    {
        private readonly IMemberRequestRepository _memberRequestRepository;

        public Handler(IMemberRequestRepository memberRequestRepository)
        {
            _memberRequestRepository = memberRequestRepository;
        }

        public async Task<PaginatedList<MemberRequestResponse>> Handle(GetMemberRequestsQuery request, CancellationToken cancellationToken)
        {
            var memberRequests = await _memberRequestRepository.GetMemberRequestsAsync(request);
            var res = memberRequests.Adapt<PaginatedList<MemberRequestResponse>>();
            return res;
        }
    }
    public record MemberRequestResponse(string Id, IReadOnlyList<MembershipInfoResponse> Requests, DateTime CreatedOn, string CreatedBy, string LastModifiedBy, DateTime LastModifiedOn);
    public record MembershipInfoResponse(string Surname, string FirstName, string AuxiliaryBodyId, string MiddleName, DateTime Dob, string Email, string PhoneNo, string JamaatId, Sex Sex, MaritalStatus MaritalStatus, string Address, EmploymentStatus EmploymentStatus, Status Status);
}
