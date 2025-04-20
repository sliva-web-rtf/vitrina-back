using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with curators.
/// </summary>
[ApiController]
[Authorize(Roles = "Curator")]
[Route("api/curators")]
[ApiExplorerSettings(GroupName = "curators")]
public class CuratorsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting curator profile data by Id.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("{curatorId:int}/profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> GetUserProfileDataById([FromRoute] int curatorId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(curatorId);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Edits curator profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("{curatorId:int}/profile/edit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> EditUserProfileById([FromRoute] int curatorId,
        [FromBody] UpdateCuratorDto curator, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UpdateUserDto>(curator);
        var command = new UpdateUserProfileCommand(curatorId, user);
        return await mediator.Send(command, cancellationToken);
    }

    [HttpGet("id/projects")]
    [Produces("application/json")]
    public virtual ICollection<ProjectDto> GetProjects([FromRoute] int id)
    {
        throw new NotImplementedException();
    }
}
