using FormBuilder.Services.FileServices;
using FormBuilder.Services.Translation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FormBuilder.Services.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureBlobStorageFileService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        services.AddOptions<AzureBlobStorageFileServiceOptions>()
            .Configure<IConfiguration>((options, configuration) =>
            {
                configuration.GetSection(AzureBlobStorageFileServiceOptions.Name).Bind(options);
            });

        switch (serviceLifetime)
        {
            case ServiceLifetime.Scoped:
                services.TryAddTransient<AzureBlobStorageFileService>();
                break;
            case ServiceLifetime.Singleton:
                services.TryAddSingleton<AzureBlobStorageFileService>();
                break;
            default:
                services.TryAddTransient<AzureBlobStorageFileService>();
                break;
        }

        return services;
    }

    public static IServiceCollection AddTranslationService(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var serviceDescriptor =
            new ServiceDescriptor(typeof(ITranslationService), typeof(TranslationService), serviceLifetime); 
        
        services.TryAdd(serviceDescriptor);
        
        return services;
    }
}