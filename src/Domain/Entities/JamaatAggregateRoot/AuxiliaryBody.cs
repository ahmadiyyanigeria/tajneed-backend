using TajneedApi.Domain.Entities.AuditTrailAggregateRoot;
using TajneedApi.Domain.Enums;

namespace TajneedApi.Domain.Entities.JamaatAggregateRoot;
public class AuxiliaryBody(string auxiliaryBodyName, Sex groupGender) : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AuxiliaryBodyName { get; private set; } = auxiliaryBodyName;
    public Sex GroupGender { get; private set; } = groupGender;

}