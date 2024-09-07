using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Infrastructure.Extensions;

public static class MigrationExtensions
{
    public static async void ApplyMigration(this IApplicationBuilder applicationBuilder)
    {
        try
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            Log.Logger.Information("Checking and Applying any pending migration.");
            await context.Database.MigrateAsync();
            //Add any data seeding method after migration 
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error occurred while migrating");
            throw;
        }

    }
}