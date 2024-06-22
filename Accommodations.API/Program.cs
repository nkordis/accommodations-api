using Accommodations.API.Middlewares;
using Accommodations.App.Extensions;
using Accommodations.Domain.Entities;
using Accommodations.Infra.Extensions;
using Accommodations.Infra.Seeders;
using Accommodations.API.Extensions;
using Serilog;


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

app.MapGroup("api/user").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
