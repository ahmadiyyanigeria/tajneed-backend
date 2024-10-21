
using TajneedApi.Application.Queries;
using static TajneedApi.Application.Commands.CreateMemberRequest;
using static TajneedApi.Application.Commands.ManageMembershipRequest;
using static TajneedApi.Application.Queries.GetMembershipRequest;
using static TajneedApi.Application.Queries.GetMembershipRequests;
namespace TajneedApi.Api.Controllers.v1;

public class MembershipRequestController : VersionedApiController
{
    [HttpPost]
    [SwaggerOperation("create member requests.")]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMembershipRequestCommand command)
    {
        var memberRequest = await Mediator.Send(command);
        if (!memberRequest.Succeeded)
            return BadRequest(memberRequest);

        return Ok(memberRequest);
    }
    [HttpPost("manage-request")]
    [SwaggerOperation("manage member requests.")]
    public async Task<IActionResult> ManageMembershipRequests([FromBody] ManageMembershipRequestCommand command)
    {
        var memberRequest = await Mediator.Send(command);
        if (!memberRequest.Succeeded)
            return BadRequest(memberRequest);
        return Ok(memberRequest);
    }
    [HttpGet("{requestId}")]
    [SwaggerOperation("get member request by id.")]
    public async Task<IActionResult> GetMemberRequest(string requestId)
    {
        var memberRequest = await Mediator.Send(new GetMemberRequestQuery { Id = requestId });
        if (!memberRequest.Succeeded)
            return NotFound(memberRequest);

        return Ok(memberRequest);
    }
    [HttpGet("get-to-be-approved-requests")]
    [SwaggerOperation("get paginated list of to be approved member requests.")]
    public async Task<IActionResult> GetToBeApprovedMemberRequests([FromQuery] GetToBeApprovedMembershipRequests query)
    {
        var memberRequest = await Mediator.Send(query);
        return Ok(memberRequest);
    }
    [HttpGet]
    [SwaggerOperation("get paginated list of member requests.")]
    public async Task<IActionResult> GetMemberRequests([FromQuery] GetMembershipRequestsQuery query)
    {
        var memberRequest = await Mediator.Send(query);
        return Ok(memberRequest);
    }

}
