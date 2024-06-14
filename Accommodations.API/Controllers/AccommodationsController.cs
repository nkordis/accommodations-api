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

        [HttpGet("{guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid guid)
        {
            var accommodation = await accommodationsService.GetAccommodation(guid);
            if (accommodation is null)
                return NotFound("The requested Id is not found");

            return Ok(accommodation);
        }
    }
}
