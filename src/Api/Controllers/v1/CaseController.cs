using static TajneedApi.Application.Commands.UpdateMemberRequest;
namespace TajneedApi.Api.Controllers.v1;

public class CaseController : VersionedApiController
{
    [HttpPost]
    [SwaggerOperation("update member requests.")]
    public async Task<IActionResult> CreateMemberRequest([FromBody] UpdateMemberRequestCommand command)
    {
        var updateMemberRequest = await Mediator.Send(command);
        if (!updateMemberRequest.Succeeded)
            return BadRequest(updateMemberRequest);

        return Ok(updateMemberRequest);
    }
    
}
