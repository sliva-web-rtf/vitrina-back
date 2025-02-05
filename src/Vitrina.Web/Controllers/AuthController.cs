using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.Auth;
using Vitrina.UseCases.Auth.ConfirmEmail;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Auth.Login;
using Vitrina.UseCases.Auth.RefreshToken;
using Vitrina.UseCases.Auth.Register;
using Vitrina.Web.Infrastructure.Web;

namespace Vitrina.Web.Controllers;

/// <summary>
/// Auth controller.
/// </summary>
[ApiController]
[Route("api/auth")]
[ApiExplorerSettings(GroupName = "auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AuthController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    /// <summary>
    /// Authenticate user by email and password.
    /// </summary>
    /// <param name="command">Login command.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPost("log-in")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<TokenModel> Authenticate([Required] LoginUserCommand command, CancellationToken cancellationToken)
    {
        return (await mediator.Send(command, cancellationToken)).TokenModel;
    }

    /// <summary>
    /// Register user.
    /// </summary>
    /// <param name="command">Register command.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPost("register")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Register([Required] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    /// <summary>
    /// Confirm email.
    /// </summary>
    [HttpPost("confirm")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ConfirmEmail([Required] ConfirmEmailCommand command,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    /// <summary>
    /// Get new token by refresh token.
    /// </summary>
    /// <param name="command">Refresh token command.</param>
    /// <returns>New authentication and refresh tokens.</returns>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPut("refresh")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    public Task<TokenModel> RefreshToken([Required] RefreshTokenCommand command, CancellationToken cancellationToken)
        => mediator.Send(command, cancellationToken);

    /// <summary>
    /// Get current logged user info.
    /// </summary>
    /// <returns>Current logged user info.</returns>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpGet("get-me")]
    [Authorize]
    public async Task<UserDetailsDto> GetMe(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery
        {
            UserId = User.GetCurrentUserId()
        };
        return await mediator.Send(query, cancellationToken);
    }
}
