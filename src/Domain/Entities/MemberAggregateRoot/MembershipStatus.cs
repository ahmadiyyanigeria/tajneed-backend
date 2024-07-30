namespace Domain.Entities;

public class MembershipStatus(string name, string createdBy) : BaseEntity(createdBy)
{
    public string Name { get; private set; } = name;
}