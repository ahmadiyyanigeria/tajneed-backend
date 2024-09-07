using Domain.Enums;

namespace Domain.Entities;
public class AuxiliaryBody(string auxiliaryBodyName, Sex groupGender, string createdBy) : BaseEntity(createdBy)
{
    public string AuxiliaryBodyName { get; private set; } = auxiliaryBodyName;
    public Sex GroupGender { get; private set; } = groupGender;

}