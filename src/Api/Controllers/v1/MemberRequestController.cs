using Microsoft.AspNetCore.Mvc;
using TajneedApi.Api.Controllers.Common;
using static TajneedApi.Application.Commands.User.CreateMemberRequest;

namespace TajneedApi.Api.Controllers.v1;

public class MemberRequestController : VersionedApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMemberRequestCommand command)
    {
        var memberRequest = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetMemberRequest), new { id = memberRequest.Id }, memberRequest);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberRequest(string id)
    {
        return Ok(id);
    }
}
