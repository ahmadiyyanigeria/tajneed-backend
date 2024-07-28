namespace Domain.Entities;

public class Position(string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
}