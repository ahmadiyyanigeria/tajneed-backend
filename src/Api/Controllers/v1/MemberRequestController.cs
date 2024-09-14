using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TajneedApi.Api.Controllers.Common;
using static TajneedApi.Application.Commands.User.CreateMemberRequest;
using static TajneedApi.Application.Queries.GetMemberPendingRequests;

namespace TajneedApi.Api.Controllers.v1;

public class MemberRequestController : VersionedApiController
{
    [HttpPost]
    [SwaggerOperation("Create member requests.")]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMemberRequestCommand command)
    {
        var memberRequest = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetMemberRequest), new { id = memberRequest.Data.Id }, memberRequest);
    }

    [HttpGet("{id}")]
    public IActionResult GetMemberRequest(string id)
    {
        return Ok(id);
    }

    [HttpGet("MemberRequests")]
    [SwaggerOperation("Create member requests.")]
    public async Task<IActionResult> GetMemberRequests([FromQuery] GetMemberRequestsQuery query)
    {
        var memberRequest = await Mediator.Send(query);
        return Ok(memberRequest);
    }
}
