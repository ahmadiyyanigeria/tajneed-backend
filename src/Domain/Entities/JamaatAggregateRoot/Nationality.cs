using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;

namespace TajneedApi.Domain.Entities.JamaatAggregateRoot;

public class Nationality(string code, string name) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;

}