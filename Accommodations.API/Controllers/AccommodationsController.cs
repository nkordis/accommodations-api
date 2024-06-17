using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.App.Accommodations.Commands.DeleteAccommodation;
using Accommodations.App.Accommodations.Queries.GetAccommodationById;
using Accommodations.App.Accommodations.Queries.GetAllAccommodations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers
{
    [ApiController]
    [Route("/api/accommodations")]
    public class AccommodationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accommodations = await mediator.Send(new GetAllAccommodationsQuery());
            return Ok(accommodations);
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] Guid guid)
        {
            var accommodation = await mediator.Send(new GetAccommodationByIdQuery(guid));

            if (accommodation is null)
                return NotFound("The requested Id is not found");

            return Ok(accommodation);
        }

        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteByGuid([FromRoute] Guid guid)
        {
            var isDeleted = await mediator.Send(new DeleteAccommodationCommand(guid));

            if (isDeleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccommodation(CreateAccommodationCommand command)
        {
            Guid guid = await mediator.Send(command);

            return CreatedAtAction(nameof(GetByGuid), new {guid}, null);
        }
    }
}
