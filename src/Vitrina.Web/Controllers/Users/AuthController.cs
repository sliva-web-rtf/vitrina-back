using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.CodeSender;
using Vitrina.UseCases.User.Auth;
using Vitrina.UseCases.User.Auth.ChangePassword;
using Vitrina.UseCases.User.Auth.ConfirmEmail;
using Vitrina.UseCases.User.Auth.GetUserById;
using Vitrina.UseCases.User.Auth.Login;
using Vitrina.UseCases.User.Auth.RecoveryPassword;
using Vitrina.UseCases.User.Auth.RefreshToken;
using Vitrina.UseCases.User.Auth.Register;
using Vitrina.UseCases.User.Auth.ResetPassword;
using Vitrina.UseCases.User.DTO;
using Vitrina.Web.Infrastructure.Web;

namespace Vitrina.Web.Controllers.Users;

/// <summary>
///     Auth controller.
/// </summary>
[ApiController]
[Route("api/auth")]
[ApiExplorerSettings(GroupName = "auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     Authenticate user by email and password.
    /// </summary>
    /// <param name="command">Login command.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPost("log-in")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<TokenModel>
        Authenticate([Required] LoginUserCommand command, CancellationToken cancellationToken) =>
        (await mediator.Send(command, cancellationToken)).TokenModel;

    /// <summary>
    ///     Register user.
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

        await mediator.Send(new SendConfirmationCodeCommand(command.Email, result.ConfirmationCode), cancellationToken);
        result.ConfirmationCode = string.Empty;

        return Ok(result);
    }

    /// <summary>
    ///     Confirm email.
    /// </summary>
    [HttpPost("confirm")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ConfirmEmail(
        [Required] ConfirmEmailCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("change-password")]
    [Authorize("Student, Curator, Partner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [Required] ChangePasswordCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return BadRequest();
        }

        return Ok(result);
    }

    [HttpPost("recover-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RecoverPassword([FromBody] string email, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GenerateTokenCommand { Email = email, UrlHelper = Url },
            cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.IsSuccess);
        }

        return Ok(result);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(
        [Required] ResetPasswordCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await mediator.Send(command, cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.IsSuccess);
        }

        return Ok(result);
    }

    /// <summary>
    ///     Get new token by refresh token.
    /// </summary>
    /// <param name="command">Refresh token command.</param>
    /// <returns>New authentication and refresh tokens.</returns>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [HttpPut("refresh")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    public Task<TokenModel> RefreshToken([Required] RefreshTokenCommand command, CancellationToken cancellationToken) =>
        mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Get current logged user info.
    /// </summary>
    /// <returns>Current logged user info.</returns>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    [Authorize]
    [HttpGet("get-me")]
    public async Task<UserDetailsDto> GetMe(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { UserId = User.GetCurrentUserId() };
        return await mediator.Send(query, cancellationToken);
    }
}
