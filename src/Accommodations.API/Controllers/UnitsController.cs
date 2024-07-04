using Accommodations.App.Units.Commands.CreateUnit;
using Accommodations.App.Units.Commands.DeleteUnit;
using Accommodations.App.Units.Dtos;
using Accommodations.App.Units.Queries.GetAllUnits;
using Accommodations.App.Units.Queries.GetUnitById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers
{
    [Route("api/accommodations/{accommodationId}/units")]
    [ApiController]
    [Authorize]
    public class UnitsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromRoute] Guid accommodationId, CreateUnitCommand command)
        {
            command.AccommodationId = accommodationId;
            Guid guid = await mediator.Send(command);

            return CreatedAtAction(nameof(GetByIdForAccommodation), new {accommodationId, guid}, null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetAllForAccommodation([FromRoute]Guid accommodationId)
        {
            var units = await mediator.Send(new GetAllUnitsQuery(accommodationId));
            return Ok(units);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<UnitDto>> GetByIdForAccommodation([FromRoute] Guid accommodationId, [FromRoute] Guid guid )
        {
            var unit = await mediator.Send(new GetUnitByIdQuery(accommodationId, guid));
            return Ok(unit);
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UnitDto>> DeleteByIdForAccommodation([FromRoute] Guid accommodationId, [FromRoute] Guid guid)
        {
            await mediator.Send(new DeleteUnitCommand(accommodationId, guid));
            return NoContent();
        }
    }
}
