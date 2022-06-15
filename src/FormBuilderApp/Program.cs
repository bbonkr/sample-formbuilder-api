
using FormBuilder.Data;
using FormBuilder.Data.Extensions.DependencyInjection;
using FormBuilder.Data.Seeders;
using FormBuilder.Domains.Extensions.DependencyInjection;
using FormBuilder.Services.Extensions.DependencyInjection;
using FormBuilderApp.Extensions.DependencyInjection;
using FormBuilderApp.Infrastructure.Filters;
using FormBuilderApp.Infrastructure.Options;
using kr.bbon.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var defaultVersion = new ApiVersion(1, 0);
var corsOptions = new CorsOptions();

var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureAppOptions();

builder.Configuration.GetSection(CorsOptions.Name).Bind(corsOptions);

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        if (isDevelopment)
        {
            options.EnableSensitiveDataLogging();
        }

        sqlServerOptions.MigrationsAssembly("FormBuilder.Data.SqlServer");
    });
});

builder.Services.AddLanguageSeeder();

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionHandlerWithLoggingFilter>();
    } )
    .ConfigureDefaultJsonOptions();

builder.Services.AddAzureBlobStorageFileService();
builder.Services.AddTranslationService();
builder.Services.AddDomainServices();
builder.Services.AddValidatorInterceptor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningAndSwaggerGen(defaultVersion);

builder.Services.AddForwardedHeaders();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        
        var allowOrigins = corsOptions.GetAllowOrigins();
        if (allowOrigins == null)
        {
            policy.AllowAnyOrigin();
        }
        else
        {
            policy.WithOrigins(allowOrigins.ToArray());
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDatabaseMigration<AppDbContext>();
app.UseDataSeeder<LanguageSeeder>();

app.UseSwaggerUIWithApiVersioning();
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwaggerUIWithApiVersioning();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
