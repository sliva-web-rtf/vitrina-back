using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Specialization;
using Vitrina.UseCases.Specialization.CreateSpecialization;
using Vitrina.UseCases.Specialization.DeleteSpecialization;
using Vitrina.UseCases.Specialization.GetAllSpecializations;

namespace Vitrina.Web.Controllers.Users;

/// <inheritdoc />
[Route("api/specializations")]
[ApiExplorerSettings(GroupName = "specializations")]
public class SpecializationsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// The method of obtaining various specializations.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<SpecializationDto[]> GetAllSpecializations(CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAllSpecializationsQuery(), cancellationToken);
    }

    /// <summary>
    /// Adding a specialization.
    /// </summary>
    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<SpecializationDto> CreateSpecialization([FromBody] CreateSpecializationCommand command,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Removing a specialization.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [HttpDelete("{specializationId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<SpecializationDto> DeleteSpecialization([FromRoute] int specializationId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteSpecializationCommand(specializationId);
        return await mediator.Send(command, cancellationToken);
    }
}
