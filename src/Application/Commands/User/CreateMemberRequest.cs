using Application.Repositories;
using Domain.Entities.JamaatAggregateRoot;
using Domain.Entities.MemberAggregateRoot;
using Domain.Enums;
using Domain.Exceptions;
using Mapster;
using MediatR;
using System.Net;

namespace Application.Commands.User;

public class CreateMemberRequest
{
    public record CreateMemberRequestCommand : IRequest<MemberRequestResponse>
    {
        public string Email { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string MiddleName { get; init; } = default!;
        public string Surname { get; init; } = default!;
        public string PhoneNo { get; init; } = default!;
        public string JamaatId { get; init; } = default!;
        public string Address { get; init; } = default!;
        public DateTime Dob { get; init; }
        public Sex Sex { get; init; }
        public MaritalStatus MaritalStatus { get; init; }
        public Status Status { get; init; }
        public EmploymentStatus EmploymentStatus { get; init; }
        //public string AuxiliaryBodyId { get; init; } = default!;
        //public AuxiliaryBody AuxiliaryBody { get; init; } = default!;
    }

    public record MemberRequestResponse(string Email, string FirstName, string MiddleName, string Surname, string PhoneNo, string JamaatId, string Address, DateTime Dob, Sex Sex, MaritalStatus MaritalStatus, Status Status, EmploymentStatus EmploymentStatus);

    public class Handler : IRequestHandler<CreateMemberRequestCommand, MemberRequestResponse>
    {
        private readonly IMemberRequestRepository _memberRequestRepository;

        public async Task<MemberRequestResponse> Handle(CreateMemberRequestCommand request, CancellationToken cancellationToken)
        {
            var memberRequest = new MemberRequest(request.Surname,request.FirstName,"AuxiliaryBody",request.MiddleName,request.Dob,request.Email,request.PhoneNo,request.JamaatId,request.Sex,request.MaritalStatus,request.Address,request.Status,request.EmploymentStatus);
             var memberRequestResponse = await _memberRequestRepository.CreateMemberRequestAsync(memberRequest);
            if (memberRequestResponse is null)
            {
                throw new DomainException($"{request} is null", ExceptionCodes.MemberRequestIsNull.ToString(), 400);
            }

            var response = memberRequestResponse.Adapt<MemberRequestResponse>();
            return response;
        }
    }
}