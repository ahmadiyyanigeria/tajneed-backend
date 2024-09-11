using Domain.Entities.AuditTrailAggregateRoot;
using Domain.Enums;

namespace Domain.Entities.JamaatAggregateRoot;
public class AuxiliaryBody(string auxiliaryBodyName, Sex groupGender) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AuxiliaryBodyName { get; private set; } = auxiliaryBodyName;
    public Sex GroupGender { get; private set; } = groupGender;

}