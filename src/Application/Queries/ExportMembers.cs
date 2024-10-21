using Microsoft.AspNetCore.Mvc;
using TajneedApi.Application.Common;
using TajneedApi.Application.Contracts;
using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Application.Queries;

public class ExportMembers
{
    public record ExportMembersQuery : PageRequest, IRequest<FileContentResult>
    {
        public string? JamaatId { get; set; }
        public string? CircuitId { get; set; }
        public string? AuxiliaryBodyId { get; set; }
        public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.Active;
    }

    public class Handler(IMemberRepository memberRepository, 
                        IExcelWriter excelWriter) : IRequestHandler<ExportMembersQuery, FileContentResult>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IExcelWriter _excelWriter = excelWriter;

        public async Task<FileContentResult> Handle(ExportMembersQuery request, CancellationToken cancellationToken)
        {
            var memberRequests = await _memberRepository.GetMembersAsync(request, request.JamaatId, request.CircuitId);
            var exportDatas = memberRequests.Adapt<PaginatedList<MemberExportData>>();
            var fileResponse = _excelWriter.GenerateCSV(exportDatas.Items.ToList(),"Members");
            return fileResponse;
        }
    }
    public record MemberExportData(
       string? ChandaNo,
       string? Surname,
       string? FirstName,
       string? MiddleName,
       string? Email,
       string? PhoneNo,
       string? MaritalStatus,
       string? Address,
       string? Sex,
       string? AimsNo,
       string? NextOfKinName,
       string? WasiyatNo,
       string? SpouseNo,
       string? RecordFlag,
       string? Occupation,
       string? NextOfKinPhoneNo,
       string? NextOfKinAddress,
       DateTime? BiatDate
   );
}
