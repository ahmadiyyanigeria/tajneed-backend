using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TajneedApi.Api.Controllers.Common;

public class VersionedApiController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}