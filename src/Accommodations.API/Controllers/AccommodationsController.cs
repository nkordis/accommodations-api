﻿using Accommodations.App.Accommodations.Commands.CreateAccommodation;
using Accommodations.App.Accommodations.Commands.DeleteAccommodation;
using Accommodations.App.Accommodations.Commands.UpdateAccommodation;
using Accommodations.App.Accommodations.Commands.UploadAccommodationImage;
using Accommodations.App.Accommodations.Dtos;
using Accommodations.App.Accommodations.Queries.GetAccommodationById;
using Accommodations.App.Accommodations.Queries.GetAllAccommodations;
using Accommodations.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommodations.API.Controllers
{
    [ApiController]
    [Route("/api/accommodations")]
    [Authorize]
    public class AccommodationsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AccommodationDto>>> GetAll([FromQuery] GetAllAccommodationsQuery query)
        {
            var accommodations = await mediator.Send(query);
            return Ok(accommodations);
        }

        [HttpGet("{guid}")]
        public async Task<ActionResult<AccommodationDto?>> GetByGuid([FromRoute] Guid guid)
        {
            var accommodation = await mediator.Send(new GetAccommodationByIdQuery(guid));
            return Ok(accommodation);
        }

        [HttpDelete("{guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteByGuid([FromRoute] Guid guid)
        {
            await mediator.Send(new DeleteAccommodationCommand(guid));
            return NoContent();
        }

        [HttpPatch("{guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAccommodation([FromRoute] Guid guid,
            UpdateAccommodationCommand command)
        {
            command.Guid = guid;
            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateAccommodation(CreateAccommodationCommand command)
        {
            Guid guid = await mediator.Send(command);

            return CreatedAtAction(nameof(GetByGuid), new { guid }, null);
        }

        [HttpPost("{guid}/image")]
        public async Task<IActionResult> UploadImage([FromRoute]Guid guid, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var command = new UploadAccommodationImageCommand()
            {
                Guid = guid,
                FileName = $"{guid}-{file.FileName}",
                File = stream
            };

            await mediator.Send(command);

            return NoContent();
        }
    }
}
