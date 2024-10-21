namespace TajneedApi.Application.Configurations;

public class ApprovalSettingsConfiguration
{
    public const string SectionName = "ApprovalSettings";
    public List<RoleSettings> Roles { get; set; } = new();

}
public class RoleSettings
{
    public string RoleName { get; set; } = default!;
    public int Level { get; set; }
}