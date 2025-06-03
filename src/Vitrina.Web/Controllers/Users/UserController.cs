using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.GetUserProjects;
using Vitrina.UseCases.User.GetUserProjectsPages;
using Vitrina.UseCases.User.GetUsers;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

[ApiController]
[Route("api/users")]
[ApiExplorerSettings(GroupName = "users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Edits student profile data.
    /// </summary>
    [ValidateUserId]
    [Authorize(Roles = "Student, Curator, Partner")]
    [Produces("application/json")]
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] JsonPatchDocument<UpdateUserDtoBase> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateUserByIdCommand(id, patchDocument);
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Retrieves the list of project pages of the user with the specified id.
    /// </summary>
    [ValidateUserId]
    [HttpGet("{id:int}/pages")]
    [Authorize(Roles = "Student, Curator")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICollection<ProjectPageDto>> GetProjectPages([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProjectPagesByUserIdQuey(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Retrieves the list of projects of the user with the specified id.
    /// </summary>
    [ValidateUserId]
    [Authorize(Roles = "Student, Curator")]
    [HttpGet("{id:int}/projects")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICollection<CreateProjectDto>> GetProjects([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProjectsByUserIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    [HttpGet("")]
    public async Task<ICollection<RequestShortenedUserDto>> GetUsers(
        [FromQuery] GetUsersQuery query,
        CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
