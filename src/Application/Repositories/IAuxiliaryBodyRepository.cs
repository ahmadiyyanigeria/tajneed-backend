using Domain.Entities.JamaatAggregateRoot;

namespace Application.Repositories;

public interface IAuxiliaryBodyRepository
{
    Task<AuxiliaryBody?> FIndAuxiliaryBodyByNameAsync(string name);
    Task<AuxiliaryBody?> FIndAuxiliaryBodyByIdAsync(string id);
}
