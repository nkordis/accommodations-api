using Accommodations.App.Accommodations;
using Accommodations.App.Accommodations.Dtos;
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
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var accommodation = await accommodationsService.GetAccommodation(guid);
            if (accommodation is null)
                return NotFound("The requested Id is not found");

            return Ok(accommodation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccommodation([FromBody]CreateAccommodationDto createAccommodationDto)
        {
            Guid guid = await accommodationsService.Create(createAccommodationDto);

            return CreatedAtAction(nameof(GetByGuid), new {guid}, null);
        }
    }
}
