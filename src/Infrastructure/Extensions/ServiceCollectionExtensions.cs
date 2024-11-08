using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TajneedApi.Application.Repositories;
using TajneedApi.Infrastructure.Persistence.Repositories;

namespace TajneedApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionStringBuilder().ConnectionString;
        return serviceCollection
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, action => action.MigrationsAssembly("TajneedApi.Infrastructure")))
            .AddApplicationServices();
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IMembershipRequestRepository, MembershipRequestRepository>()
            .AddScoped<ICaseRepository, CaseRepository>()
            .AddScoped<IDatabaseInitializer, DatabaseInitializer>()
            .AddScoped<IMemberRepository, MemberRepository>()
            .AddScoped<IAuxiliaryBodyRepository, AuxiliaryBodyRepository>();
    }
}