using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
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
        public MembershipStatus? MembershipStatus { get; set; }
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
            var fileResponse = _excelWriter.GenerateCSV(exportDatas.Items.ToList(), "Members");
            return fileResponse;
        }
    }

    public class MemberExportData
    {
        [DisplayName("Chanda No")]
        public string? ChandaNo { get; set; }

        [DisplayName("Surname")]
        public string? Surname { get; set; }

        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }

        [DisplayName("Email Address")]
        public string? Email { get; set; }

        [DisplayName("Phone Number")]
        public string? PhoneNo { get; set; }

        [DisplayName("Marital Status")]
        public string? MaritalStatus { get; set; }

        [DisplayName("Status")]
        public string? MembershipStatus { get; set; }

        [DisplayName("Address")]
        public string? Address { get; set; }

        [DisplayName("Gender")]
        public string? Sex { get; set; }

        [DisplayName("Aims No")]
        public string? AimsNo { get; set; }

        [DisplayName("Wasiyat No")]
        public string? WasiyatNo { get; set; }

        [DisplayName("Occupation")]
        public string? Occupation { get; set; }

        [DisplayName("Biat Date")]
        public DateTime? BiatDate { get; set; }

        // [DisplayName("Next of Kin Name")]
        // public string? NextOfKinName { get; set; }

        // [DisplayName("Spouse Number")]
        // public string? SpouseNo { get; set; }

        // [DisplayName("Record Flag")]
        // public string? RecordFlag { get; set; }

        // [DisplayName("Next of Kin Phone Number")]
        // public string? NextOfKinPhoneNo { get; set; }

        // [DisplayName("Next of Kin Address")]
        // public string? NextOfKinAddress { get; set; }
    }
}
