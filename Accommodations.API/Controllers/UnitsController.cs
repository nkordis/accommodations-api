using Accommodations.App.Units.Commands.CreateUnit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers
{
    [Route("api/accommodations/{accommodationId}/units")]
    [ApiController]
    public class UnitsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromRoute] Guid accommodationId, CreateUnitCommand command)
        {
            command.AccommodationId = accommodationId;
            Guid guid = await mediator.Send(command);

            return Created();
        }
    }
}
