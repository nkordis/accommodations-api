using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthCheckController : ControllerBase
{
    private readonly ILogger<HealthCheckController> _logger;

    public HealthCheckController(ILogger<HealthCheckController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check endpoint hit");
        return Ok(new { Status = "Healthy", Message = "API is up and running." });
    }
}
