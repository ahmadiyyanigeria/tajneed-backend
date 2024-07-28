using Domain.Enums;

namespace Domain.Entities;
public class AuxillaryBody(string auxillaryBodyName, Sex groupGender, string createdBy) : BaseEntity(createdBy)
{
    public string AuxillaryBodyName { get; private set; } = auxillaryBodyName;
    public Sex GroupGender { get; private set; } = groupGender;

}