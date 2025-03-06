using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Vitrina.Domain.User;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.UserProfile.GetUserById;

namespace Vitrina.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
[ApiExplorerSettings(GroupName = "users")]
public class UsersController(IMediator mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Getting user profile data by Id.
    /// </summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [HttpGet("{userId:int}/profile")]
    public async Task<ActionResult<JsonContent>> GetUserProfileDataById([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { UserId = userId };
        var user = await mediator.Send(query, cancellationToken);
        return user is null ? NotFound("The user with the specified Id was not found") : Ok(user.ProfileData.ToString());
    }

    /// <summary>
    /// Edits user profile data.
    /// </summary>
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(422)]
    [Produces("application/json")]
    [HttpPatch("{userId:int}/profile/edit")]
    public async Task<ActionResult<JsonContent>> EditUserProfileById([FromRoute] int userId,
        [FromBody] UpdatedUserDto? userDto, CancellationToken cancellationToken)
    {
        if (userDto is null)
        {
            return BadRequest("Невалидный JSON");
        }

        var user = await mediator.Send(new GetUserByIdQuery { UserId = userId }, cancellationToken);
        if (user is null)
        {
            return NotFound("The user with the specified Id was not found");
        }

        if (userDto.EducationLevel is not null || userDto.EducationCourse is not null)
        {
            var educationLevel = userDto.EducationLevel ?? user.EducationLevel;
            var educationCourse = userDto.EducationCourse ?? user.EducationCourse;
            if (CheckEducationCourse(educationCourse, educationLevel))
            {
                return UnprocessableEntity("The education course does not correspond to the education level.");
            }
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        mapper.Map(userDto, user);
        return Ok(user.ProfileData);
    }

    private bool CheckEducationCourse(int educationCourse, EducationLevelEnum educationLevel)
    {
        return educationLevel switch
        {
            EducationLevelEnum.Bachelors => educationCourse is > 0 and < 5,
            EducationLevelEnum.Specialty => educationCourse is > 0 and < 6,
            EducationLevelEnum.Magistracy => educationCourse is > 0 and < 3,
            EducationLevelEnum.Postgraduate => educationCourse is > 0 and < 5,
            EducationLevelEnum.NotStudent => true,
            _ => false
        };
    }
}
