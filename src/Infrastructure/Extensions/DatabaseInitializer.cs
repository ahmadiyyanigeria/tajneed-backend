using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TajneedApi.Application.Repositories;
using TajneedApi.Application.ServiceHelpers;
using TajneedApi.Domain.Constants;
using TajneedApi.Domain.Entities.JamaatAggregateRoot;
using TajneedApi.Domain.Enums;
using TajneedApi.Infrastructure.Persistence.Repositories;

namespace TajneedApi.Infrastructure.Extensions;

public class DatabaseInitializer(ApplicationDbContext context) : IDatabaseInitializer
{
    private readonly ApplicationDbContext _context = context;

    public async Task SeedDatas()
    {
        bool auxiliaries = await _context.AuxiliaryBodies.AnyAsync();
        if (!auxiliaries)
        {
            await AddAuxiliaries();
            await AddMockJamaat();//should be remove later

        }
    }

    private async Task AddAuxiliaries()
    {
        var ansarullah = new AuxiliaryBody(AuxiliaryBodyName.Ansarullah, Sex.Male);
        var atfal = new AuxiliaryBody(AuxiliaryBodyName.Atfal, Sex.Male);
        var khuddam = new AuxiliaryBody(AuxiliaryBodyName.Khuddam, Sex.Male);
        var lajna = new AuxiliaryBody(AuxiliaryBodyName.Lajna, Sex.Female);
        var nasirat = new AuxiliaryBody(AuxiliaryBodyName.Nasirat, Sex.Female);
        var auxiliaryBodies = new List<AuxiliaryBody> { ansarullah, atfal, khuddam, lajna, nasirat };
        await _context.AuxiliaryBodies.AddRangeAsync(auxiliaryBodies);
        await _context.SaveChangesAsync();
    }

    private async Task AddMockJamaat()
    {
        var circuit = new Circuit("0", "Test Circuit");
        var jamaat = new Jamaat("Test Jamat", "0", circuit.Id);
        await _context.Circuits.AddAsync(circuit);
        await _context.Jamaats.AddAsync(jamaat);
        await _context.SaveChangesAsync();
    }
}