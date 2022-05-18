
using FormBuilder.Data;
using FormBuilder.Domains.Extensions.DependencyInjection;
using FormBuilder.Services.Extensions.DependencyInjection;
using FormBuilderApp.Extensions.DependencyInjection;
using FormBuilderApp.Infrastructure.Filters;
using kr.bbon.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var defaultVersion = new ApiVersion(1, 0);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureAppOptions();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly("FormBuilder.Data.SqlServer");
    });
});

builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ApiExceptionHandlerWithLoggingFilter>();
    } )
    .ConfigureDefaultJsonOptions();

builder.Services.AddAzureBlobStorageFileService();
builder.Services.AddDomainServices();
builder.Services.AddValidatorInterceptor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersioningAndSwaggerGen(defaultVersion);

builder.Services.AddForwardedHeaders();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseDatabaseMigration<AppDbContext>();
app.UseSwaggerUIWithApiVersioning();
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwaggerUIWithApiVersioning();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
