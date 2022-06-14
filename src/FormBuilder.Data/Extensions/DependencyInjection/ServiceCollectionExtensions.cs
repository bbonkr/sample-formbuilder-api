using FormBuilder.Data.Seeders;
using Microsoft.Extensions.DependencyInjection;

namespace FormBuilder.Data.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLanguageSeeder(this IServiceCollection services)
    {
        services.AddScoped<LanguageSeeder>();

        return services;
    }
}