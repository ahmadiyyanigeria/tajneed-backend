using Microsoft.EntityFrameworkCore;
using TajneedApi.Application.Repositories;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;

namespace TajneedApi.Infrastructure.Persistence.Repositories;

public class AuxiliaryBodyRepository(ApplicationDbContext context) : IAuxiliaryBodyRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<AuxiliaryBody?> FIndAuxiliaryBodyByNameAsync(string name)
    {
        return await _context.AuxiliaryBodies.FirstOrDefaultAsync(f => f.AuxiliaryBodyName == name);
    }

    public async Task<AuxiliaryBody?> FIndAuxiliaryBodyByIdAsync(string id)
    {
        return await _context.AuxiliaryBodies.FirstOrDefaultAsync(f => f.Id == id);
    }
}
