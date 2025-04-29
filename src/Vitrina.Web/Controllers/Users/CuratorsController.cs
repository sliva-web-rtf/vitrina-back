using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;
using UserDto = Vitrina.UseCases.User.DTO.UserDto;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
///     A controller for working with curators.
/// </summary>
[ApiController]
[Authorize(Roles = "Curator")]
[Route("api/curators/{id:int}")]
[ApiExplorerSettings(GroupName = "curators")]
public class CuratorsController(IMediator mediator, IMapper mapper) : ObtainingProjectInformationBase(mediator)
{
    /// <summary>
    ///     Getting curator profile data by Id.
    /// </summary>
    [HttpGet("profile")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> GetUserProfileDataById([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Edits curator profile data.
    /// </summary>
    [HttpPatch("profile")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> EditUserProfileById([FromRoute] int id,
        [FromBody] CuratorDto curator, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UserDto>(curator);
        var command = new UpdateUserProfileCommand(id, user);
        return await mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    [HttpGet("projects")]
    [Produces("application/json")]
    public override async Task<ICollection<PreviewProjectDto>> GetProjects([FromRoute] int id,
        CancellationToken cancellationToken) => await base.GetProjects(id, cancellationToken);

    /// <inheritdoc />
    [HttpGet("pages")]
    [Produces("application/json")]
    public override async Task<ICollection<ProjectPageDto>> GetProjectPages([FromRoute] int id,
        CancellationToken cancellationToken) => await base.GetProjectPages(id, cancellationToken);
}
