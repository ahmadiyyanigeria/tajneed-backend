using static TajneedApi.Application.Queries.ExportMembers;
using static TajneedApi.Application.Queries.GetMember;
using static TajneedApi.Application.Queries.GetMembers;
namespace TajneedApi.Api.Controllers.v1;

public class MemberController : VersionedApiController
{
    [HttpGet("{requestId}")]
    [SwaggerOperation("get member by id.")]
    public async Task<IActionResult> GetMember(string requestId)
    {
        var memberRequest = await Mediator.Send(new GetMemberQuery { Id = requestId });
        if (!memberRequest.Succeeded)
            return NotFound(memberRequest);

        return Ok(memberRequest);
    }

    [HttpGet]
    [SwaggerOperation("get paginated list of members.")]
    public async Task<IActionResult> GetMembers([FromQuery] GetMembersQuery query)
    {
        var memberRequest = await Mediator.Send(query);
        return Ok(memberRequest);
    }
    
    [HttpGet("ExportMembers")]
    [SwaggerOperation("Export members to CSV, Excel or PDF.","Export are only available for suported file type")]
    public async Task<IActionResult> ExportMembers([FromQuery] ExportMembersQuery query)
    {
        var memberRequest = await Mediator.Send(query);
        return memberRequest;
    }

}
