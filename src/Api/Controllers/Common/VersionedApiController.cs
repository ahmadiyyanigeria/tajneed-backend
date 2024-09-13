using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TajneedApi.Api.Controllers.Common;
/// <summary>
/// This controller provides values-related actions.
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class VersionedApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}