using Accommodations.API.Configurations;
using Accommodations.App.Extensions;
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
});

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

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(swaggerSettings.Endpoint, $"{swaggerSettings.Title} {swaggerSettings.Version}");
    c.RoutePrefix = swaggerSettings.RoutePrefix;
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
