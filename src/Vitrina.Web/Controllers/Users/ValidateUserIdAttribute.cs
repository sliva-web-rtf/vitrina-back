using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
/// Checks whether the id under which the user is logged in matches the id of the user whose data he is trying to change
/// </summary>
public class ValidateUserIdAttribute : Attribute, IAuthorizationFilter
{
    /// <inheritdoc />
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!context.RouteData.Values.TryGetValue("id", out var routeUserId))
        {
            context.Result = new BadRequestObjectResult($"Route parameter id is missing.");
            return;
        }

        if (userIdClaim != routeUserId?.ToString())
        {
            context.Result = new ForbidResult();
        }
    }
}
