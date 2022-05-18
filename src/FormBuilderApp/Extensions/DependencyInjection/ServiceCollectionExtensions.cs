using FluentValidation.AspNetCore;
using FormBuilderApp.Infrastructure.Validations;
using MediatR;

namespace FormBuilderApp.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidatorInterceptor(this IServiceCollection services)
    {
        services.AddTransient<IValidatorInterceptor, ValidatorInterceptor>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}