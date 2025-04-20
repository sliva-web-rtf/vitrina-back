using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with students.
/// </summary>
[Authorize(Roles = "Student")]
[Route("api/students/{id:int}")]
[ApiExplorerSettings(GroupName = "students")]
public class StudentsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting student profile data by ID.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> GetStudentProfileDataById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetUserProfileByIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Edits student profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> EditStudentProfileById([FromRoute] int id,
        [FromBody] UpdateStudentDto student, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UpdateUserDto>(student);
        var command = new UpdateUserProfileCommand(id, user);
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Retrieves the list of projects of the user with the specified id.
    /// </summary>
    [HttpGet("projects")]
    [Produces("application/json")]
    public async Task<ICollection<ProjectDto>> GetProjects([FromRoute] int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves the list of project pages of the user with the specified id.
    /// </summary>
    [HttpGet("pages")]
    [Produces("application/json")]
    public async Task<ICollection<ProjectPageDto>> GetProjectPages([FromRoute] int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
