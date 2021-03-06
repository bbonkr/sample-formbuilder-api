using FormBuilder.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace FormBuilderApp.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDatabaseMigration<TDbContext>(this IApplicationBuilder builder) where TDbContext : DbContext
    {
        using (var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
            dbContext.Database.Migrate();
        }

        return builder;
    }

    public static IApplicationBuilder UseDataSeeder<TDataSeeder>(this IApplicationBuilder builder)
        where TDataSeeder : DataSeederBase
    {
        using (var scope = builder.ApplicationServices.CreateScope())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<TDataSeeder>();
            seeder.Seed();
        }

        return builder;
    }
}