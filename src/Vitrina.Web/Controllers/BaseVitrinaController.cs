using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Vitrina.Web.Controllers;

public class BaseVitrinaController : ControllerBase
{
    protected int GetIdAuthorizedUser()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null || !int.TryParse(claim.Value, out var userId))
        {
            throw new InvalidOperationException("Пользователь не авторизирован.");
        }

        return userId;
    }
}
