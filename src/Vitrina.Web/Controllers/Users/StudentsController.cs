using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUser.GetStudentById;
using Vitrina.UseCases.User.UpdateUser.UpdateStudent;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// A controller for working with students.
/// </summary>
[Authorize(Roles = "Student")]
[Route("api/students")]
[ApiExplorerSettings(GroupName = "students")]
public class StudentsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Getting student profile data by ID.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("{studentId:int}/profile")]
    public async Task<StudentDto> GetStudentProfileDataById([FromRoute] int studentId, CancellationToken cancellationToken)
    {
        var query = new GetStudentByIdQuery(studentId);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Edits student profile data.
    /// </summary>
    [Produces("application/json")]
    [HttpPatch("{studentId:int}/profile/edit")]
    public async Task<StudentDto> EditStudentProfileById([FromRoute] int studentId,
        [FromBody] JsonPatchDocument<UpdateStudentDto> patchDocument, CancellationToken cancellationToken)
    {
        var command = new UpdateStudentCommand(studentId, patchDocument);
        return await mediator.Send(command, cancellationToken);
    }
}
