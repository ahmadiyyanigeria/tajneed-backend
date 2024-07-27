namespace Domain.Entities;

public class HouseHold(string address, string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
    public string Address { get; private set; } = address;
  
}