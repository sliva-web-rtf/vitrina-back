using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.UseCases.UserProfile.GetUserById;

namespace Vitrina.Web.Controllers;

[ApiController]
[Route("api/profile")]
[ApiExplorerSettings(GroupName = "profile")]
public class UserProfileController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Getting user profile data by Id.
    /// </summary>
    [Produces("application/json")]
    [HttpGet("{userId}", Name = nameof(GetProfileDataById))]
    public async Task<ActionResult<JsonContent>> GetProfileDataById([FromRoute] int userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { UserId = userId };
        var user = await mediator.Send(query, cancellationToken);
        return user is null ? NotFound("The user with the specified Id was not found") : Ok(user.ProfileData.ToString());
    }
}
