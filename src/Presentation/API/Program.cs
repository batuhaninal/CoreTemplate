using Adapter;
using API.Extensions;
using Application;
using Application.Utilities.FluentValidations.Categories;
using FluentValidation.AspNetCore;
using Persistence;
using OpenTelemetry.Shared;
using API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestResponseActivityMiddleware>();

//app.UseMiddleware<CustomExceptionMiddleware>();

// Auto migration
await app.ConfigureMigrationAsync();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();
