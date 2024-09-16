
using static TajneedApi.Application.Commands.User.CreateMemberRequest;
using static TajneedApi.Application.Queries.GetMemberPendingRequests;
using static TajneedApi.Application.Queries.GetPendingMemberRequest;

namespace TajneedApi.Api.Controllers.v1;

public class MemberRequestController : VersionedApiController
{
    [HttpPost]
    [SwaggerOperation("Create member requests.")]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMemberRequestCommand command)
    {
        var memberRequest = await Mediator.Send(command);
        return CreatedAtAction(nameof(CreateMemberRequest), new { requestId = memberRequest.Data.Id }, memberRequest);
    }

    [HttpGet("{requestId}")]
    public async Task<IActionResult> GetMemberRequest(string requestId)
    {
        var memberRequest = await Mediator.Send(new GetMemberRequestQuery { Id = requestId });
        if (!memberRequest.Succeeded)
            return NotFound(memberRequest);

        return Ok(memberRequest);
    }

    [HttpGet]
    [SwaggerOperation("get paginated list of member requests.")]
    public async Task<IActionResult> GetMemberRequests([FromQuery] GetMemberRequestsQuery query)
    {
        var memberRequest = await Mediator.Send(query);
        return Ok(memberRequest);
    }
}
