using Application.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionStringBuilder().ConnectionString;
        return serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, action => action.MigrationsAssembly("Infrastructure")))
            .AddApplicationServices();
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IMemberRequestRepository, MemberRequestRepository>()
            .AddScoped<IAuxiliaryBodyRepository, AuxiliaryBodyRepository>();
    }
}