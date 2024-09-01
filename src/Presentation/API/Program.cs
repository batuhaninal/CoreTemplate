using Adapter;
using API.Extensions;
using Application;
using Application.Utilities.FluentValidations.Categories;
using FluentValidation.AspNetCore;
using Persistence;
using OpenTelemetry.Shared;
using API.Middlewares;
using HealthChecks.UI.Client;
using Serilog;
using Asp.Versioning;
using API.OpenApi;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using API.Controllers.v2;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

// Add services to the container.
builder.Services.BindApplicationServices(builder.Configuration);
builder.Services.BindAdapterServices(builder.Configuration);
builder.Services.BindPersistenceServices(builder.Configuration);

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateCategoryDtoValidator>());


builder.Services.ConfigureRateLimiting();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureJwtAuth(builder.Configuration);

builder.Services.AddOpenTelemetryExtension(builder.Configuration);

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(2);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version")
            );
})
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (var item in descriptions)
        {
            string url = $"/swagger/{item.GroupName}/swagger.json";
            string name = item.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseMiddleware<RequestResponseActivityMiddleware>();

app.UseMiddleware<CustomExceptionMiddleware>();

// Auto migration
await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.MapHealthChecks("health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

ApiVersionSet v2Set = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(2))
                .ReportApiVersions()
                .Build();

var group = app.MapGroup("api/v{v:apiVersion}").WithApiVersionSet(v2Set);

group.MapArticlesEndpoints();

app.Run();
