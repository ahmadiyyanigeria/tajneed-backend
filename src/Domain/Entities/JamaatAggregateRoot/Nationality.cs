using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.JamaatAggregateRoot;

public class Nationality(string code, string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
    public string Code { get; private set; } = code;

}