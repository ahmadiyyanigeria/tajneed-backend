using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TajneedApi.Application.Repositories;
using TajneedApi.Infrastructure.Persistence.Repositories;

namespace TajneedApi.Infrastructure.Extensions;

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
            if (!context.Jamaats.Any())
            {
                await serviceScope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().SeedDatas();
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "Error occurred while migrating");
            throw;
        }

    }
}
