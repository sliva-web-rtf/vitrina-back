using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Vitrina.UseCases.User.Auth.Login;

/// <summary>
///     Handler for <see cref="LoginUserCommand" />.
/// </summary>
internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserCommandResult>
{
    private readonly ILogger<LoginUserCommandHandler> logger;
    private readonly SignInManager<Domain.User.User> signInManager;
    private readonly IAuthenticationTokenService tokenService;

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="signInManager">Sign in manager.</param>
    /// <param name="tokenService">Token service.</param>
    /// <param name="logger">Logger.</param>
    public LoginUserCommandHandler(
        SignInManager<Domain.User.User> signInManager,
        IAuthenticationTokenService tokenService,
        ILogger<LoginUserCommandHandler> logger)
    {
        this.signInManager = signInManager;
        this.tokenService = tokenService;
        this.logger = logger;
    }

    /// <inheritdoc />
    public async Task<LoginUserCommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Password sign in.
        var result = await signInManager.PasswordSignInAsync(request.Email, request.Password,
            lockoutOnFailure: false, isPersistent: request.RememberMe);
        ValidateSignInResult(result, request.Email);

        // Get user and log.
        var user = await signInManager.UserManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new ArgumentException($"User with email {request.Email} not found.");
        }

        if (!user.EmailConfirmed)
        {
            throw new ArgumentException($"User with email {request.Email} not confirmed.");
        }

        logger.LogInformation("User with email {email} has logged in.", user.Email);

        // Update last login date.
        user.LastLogin = DateTime.UtcNow;
        await signInManager.UserManager.UpdateAsync(user);

        // Combine refresh token with user id.
        var principal = await signInManager.CreateUserPrincipalAsync(user);

        // Give token.
        return new LoginUserCommandResult
        {
            UserId = user.Id, TokenModel = TokenModelGenerator.Generate(tokenService, principal.Claims)
        };
    }

    private void ValidateSignInResult(SignInResult signInResult, string email)
    {
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed)
            {
                throw new ArgumentException($"User {email} is not allowed to Sign In.");
            }

            if (signInResult.IsLockedOut)
            {
                throw new ArgumentException($"User {email} is locked out.");
            }

            throw new ArgumentException("Email or password is incorrect.");
        }
    }
}
