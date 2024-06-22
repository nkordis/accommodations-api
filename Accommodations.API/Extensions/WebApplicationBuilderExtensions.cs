using Accommodations.API.Configurations;
using Accommodations.API.Middlewares;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;

namespace Accommodations.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    // Property to hold Swagger settings
    public static SwaggerSettings? SwaggerSettings { get; private set; }

    // Property to hold Serilog settings
    public static SerilogSettings? SerilogSettings { get; private set; }

    // Method to add presentation layer services and configurations
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        // Load Swagger settings from configuration
        SwaggerSettings = builder.Configuration.GetSection("Swagger")?.Get<SwaggerSettings>()
            ?? throw new InvalidOperationException("Swagger settings are not configured properly.");

        // Load Serilog settings from configuration
        SerilogSettings = builder.Configuration.GetSection("Serilog").Get<SerilogSettings>()
            ?? throw new InvalidOperationException("Serilog settings are not configured properly.");

        // Add controllers with Newtonsoft JSON options
        builder.Services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        });

        // Configure Swagger generation
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(SwaggerSettings.Version, new OpenApiInfo
            {
                Version = SwaggerSettings.Version,
                Title = SwaggerSettings.Title,
                Description = SwaggerSettings.Description,
            });

            // Add security definition for Bearer authentication
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

        // Add API explorer for minimal APIs
        builder.Services.AddEndpointsApiExplorer();

        // Register custom middlewares
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

        // Configure Serilog for logging
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
    }
}

