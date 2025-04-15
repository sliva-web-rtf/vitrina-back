using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser;
using Vitrina.UseCases.User.UpdateUser;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with students.
/// </summary>
[Authorize(Roles = "Student")]
[Route("api/students/{id:int}/profile")]
[ApiExplorerSettings(GroupName = "students")]
public class StudentsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting student profile data by ID.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("")]
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
    [HttpPatch("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<JsonDocument> EditStudentProfileById([FromRoute] int id,
        [FromBody] UpdateStudentDto student, CancellationToken cancellationToken)
    {
        var user = mapper.Map<UpdateUserDto>(student);
        var command = new UpdateUserProfileCommand(id, user);
        return await mediator.Send(command, cancellationToken);
    }
}
