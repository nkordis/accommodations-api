using Accommodations.App.Accommodations;
using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers
{
    [ApiController]
    [Route("/api/accommodations")]
    public class AccommodationsController(IAccommodationsService accommodationsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accommodations = await accommodationsService.GetAllAccommodations();
            return Ok(accommodations);
        }
    }
}
