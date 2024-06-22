using Accommodations.API.Configurations;
using Accommodations.API.Middlewares;
using Accommodations.App.Extensions;
using Accommodations.Domain.Entities;
using Accommodations.Infra.Extensions;
using Accommodations.Infra.Seeders;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var serilogSettings = builder.Configuration.GetSection("Serilog").Get<SerilogSettings>()
    ?? throw new InvalidOperationException("Serilog settings are not configured properly.");

var swaggerSettings = builder.Configuration.GetSection("Swagger")?.Get<SwaggerSettings>() 
    ?? throw new InvalidOperationException("Swagger settings are not configured properly.");

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(swaggerSettings.Version, new OpenApiInfo
    {
        Version = swaggerSettings.Version,
        Title = swaggerSettings.Title,
        Description = swaggerSettings.Description,
    });

    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme 
    { 
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
            },
            []
        }
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

builder.Services.AddApplication();
builder.Services.AddDbInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IAccommodationSeeder>();

await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(swaggerSettings.Endpoint, $"{swaggerSettings.Title} {swaggerSettings.Version}");
    c.RoutePrefix = swaggerSettings.RoutePrefix;
});


app.UseHttpsRedirection();

app.MapGroup("api/user").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
