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
using UserDto = Vitrina.UseCases.User.DTO.UserDto;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with students.
/// </summary>
[Authorize(Roles = "Student")]
[Route("api/students/{id:int}")]
[ApiExplorerSettings(GroupName = "students")]
public class StudentsController(IMediator mediator, IMapper mapper) : ObtainingProjectInformationBase(mediator)
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
        [FromBody] StudentDto student, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UserDto>(student);
        var command = new UpdateUserProfileCommand(id, user);
        return await mediator.Send(command, cancellationToken);
    }

    /// <inheritdoc />
    [HttpGet("projects")]
    [Produces("application/json")]
    public override async Task<ICollection<PreviewProjectDto>> GetProjects([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await base.GetProjects(id, cancellationToken);
    }

    /// <inheritdoc />
    [HttpGet("pages")]
    [Produces("application/json")]
    public override async Task<ICollection<ProjectPageDto>> GetProjectPages([FromRoute] int id, CancellationToken cancellationToken)
    {
        return await base.GetProjectPages(id, cancellationToken);
    }
}
