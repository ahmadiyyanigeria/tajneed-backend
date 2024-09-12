namespace TajneedApi.Application.Repositories;

public interface ICurrentUser
{
    bool IsAuthenticated();
    bool IsInRole(string role);
    UserDetails GetUserDetails();
}
public record UserDetails
{
    public string UserId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Department { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
}
