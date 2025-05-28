using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.GetUserProjects;
using Vitrina.UseCases.User.GetUserProjectsPages;
using Vitrina.UseCases.User.GetUsers;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

[ApiController]
[Route("api/users/{id:int}")]
[ApiExplorerSettings(GroupName = "users")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStudentProfileDataById([FromRoute] int id, CancellationToken cancellationToken)
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
    [HttpPatch("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditStudentProfileById([FromRoute] int id,
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
    [HttpGet("pages")]
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
    [HttpGet("projects")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ICollection<ProjectDto>> GetProjects([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProjectsByUserIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    [HttpGet("")]
    public async Task<ICollection<RequestShortenedUserDto>> GetUsersById(
        [FromQuery] GetUsersQuery query,
        CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
