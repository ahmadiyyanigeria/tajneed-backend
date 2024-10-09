using Application.Extensions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TajneedApi.Application.Behaviours;
using TajneedApi.Application.Commands;
using TajneedApi.Application.Configurations;
using TajneedApi.Application.Extensions;

namespace TajneedApi.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
    {
        // Set FluentValidator Global Options
        ValidatorOptions.Global.DisplayNameResolver = (_, member, _) => member.Name.ToCamelCase();
        ValidatorOptions.Global.PropertyNameResolver = (_, member, _) => member.Name.ToCamelCase();

        return serviceCollection
            .AddValidatorsFromAssemblyContaining<CreateMemberRequest.CreateMemberRequestCommandValidator>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }

    public static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(StringExtensions).Assembly));
    }
    

}