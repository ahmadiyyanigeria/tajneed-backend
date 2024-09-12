using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Application.Repositories;

public interface IAuxiliaryBodyRepository
{
    Task<AuxiliaryBody?> FIndAuxiliaryBodyByNameAsync(string name);
    Task<AuxiliaryBody?> FIndAuxiliaryBodyByIdAsync(string id);
}
