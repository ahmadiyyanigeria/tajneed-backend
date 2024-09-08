using Application.Repositories;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Persistence.Repositories;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private ClaimsPrincipal? User => _httpContextAccessor?.HttpContext?.User;

    public bool IsAuthenticated() =>
    User?.Identity?.IsAuthenticated is true;

    public bool IsInRole(string role) =>
        User?.IsInRole(role) is true;

    public UserDetails GetUserDetails()
    {
        return new UserDetails
        {
            UserId = GetClaimValue(ClaimTypes.NameIdentifier),
            Name = GetClaimValue(ClaimTypes.Name),
            Email = GetClaimValue(ClaimTypes.Email),
            Role = GetClaimValue(ClaimTypes.Role)
        };
    }

    private string GetClaimValue(string claimType) => User?.FindFirst(claimType)?.Value ?? string.Empty;
}
