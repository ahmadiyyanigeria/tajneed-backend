using Microsoft.AspNetCore.Mvc;
using TajneedApi.Api.Controllers.Common;

namespace TajneedApi.Api.Controllers.v1;

public class MemberController : VersionedApiController
{
    [HttpPost]
    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
}
