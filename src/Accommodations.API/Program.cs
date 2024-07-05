using Accommodations.API.Middlewares;
using Accommodations.App.Extensions;
using Accommodations.Domain.Entities;
using Accommodations.Infra.Extensions;
using Accommodations.Infra.Seeders;
using Accommodations.API.Extensions;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddDbInfrastructure(builder.Configuration);

    var app = builder.Build();

    // Seed the Database with initial data
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetRequiredService<IAccommodationSeeder>();
        await seeder.Seed();
    }

    // Configure the HTTP request pipeline.
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();

    app.UseSerilogRequestLogging();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var swaggerSettings = WebApplicationBuilderExtensions.SwaggerSettings;
        c.SwaggerEndpoint(swaggerSettings!.Endpoint, $"{swaggerSettings.Title} {swaggerSettings.Version}");
        c.RoutePrefix = swaggerSettings.RoutePrefix;
    });


    app.UseHttpsRedirection();

    app.MapGroup("api/user")
        .WithTags("User")
        .MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}

// Defining Program class as public using partial keyword
// This allows us to reference the Program class in our integration tests,
// specifically for setting up the WebApplicationFactory, which requires a public class type.
// This trick enables us to create an in-memory version of our API for integration testing.
public partial class Program { }
