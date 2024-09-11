using Domain.Entities.AuditTrailAggregateRoot;

namespace Domain.Entities.HouseHoldAggregateRoot;

public class HouseHold(string address, string name) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
    public string Address { get; private set; } = address;

}