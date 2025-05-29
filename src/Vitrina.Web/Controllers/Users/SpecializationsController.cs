using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.UserSpecialization;
using Vitrina.UseCases.UserSpecialization.CreateSpecialization;
using Vitrina.UseCases.UserSpecialization.DeleteSpecialization;
using Vitrina.UseCases.UserSpecialization.GetAllSpecializations;

namespace Vitrina.Web.Controllers.Users;

/// <inheritdoc />
[Route("api/specializations")]
[ApiExplorerSettings(GroupName = "specializations")]
public class SpecializationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Get all the specializations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<SpecializationDto[]> GetAllSpecializations(CancellationToken cancellationToken) =>
        await mediator.Send(new GetAllSpecializationsQuery(), cancellationToken);

    /// <summary>
    ///     Adding a specialization.
    /// </summary>
    [HttpPost("")]
    [Authorize(Roles = "Administrator")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSpecialization([FromBody] CreateSpecializationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        return Created($"api/specializations/{result.Id}", result);
    }

    /// <summary>
    ///     Removing a specialization.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    [Produces("application/json")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<SpecializationDto> DeleteSpecialization([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpecializationCommand(id);
        return await mediator.Send(command, cancellationToken);
    }
}
