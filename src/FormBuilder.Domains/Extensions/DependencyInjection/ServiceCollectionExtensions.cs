using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FormBuilder.Domains.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        var thisAssembly = typeof(PlaceHolder).Assembly;

        var assemblies = new List<Assembly>
        {
            thisAssembly,
        };
    
        services.AddAutoMapper(assemblies);
        services.AddMediatR(assemblies.ToArray());
        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}