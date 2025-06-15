using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.User.Auth.ConfirmEmail;

/// <summary>
///     Handler for <see cref="ConfirmEmailCommand" />
/// </summary>
public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailCommandResult>
{
    private readonly IAppDbContext appDbContext;
    private readonly SignInManager<Domain.User.User> signInManager;
    private readonly IAuthenticationTokenService tokenService;
    private readonly UserManager<Domain.User.User> userManager;

    /// <summary>
    ///     Constructor.
    /// </summary>
    public ConfirmEmailCommandHandler(IAppDbContext appDbContext, SignInManager<Domain.User.User> signInManager,
        IAuthenticationTokenService tokenService, UserManager<Domain.User.User> userManager)
    {
        this.appDbContext = appDbContext;
        this.signInManager = signInManager;
        this.tokenService = tokenService;
        this.userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<ConfirmEmailCommandResult> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appDbContext.Users.FirstAsync(u => u.Id == request.UserId, cancellationToken);
        await userManager.ConfirmEmailAsync(user, request.ConfirmationCode);
        var principal = await signInManager.CreateUserPrincipalAsync(user);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return new ConfirmEmailCommandResult
        {
            IsSuccess = true, UserId = user.Id, Token = TokenModelGenerator.Generate(tokenService, principal.Claims)
        };
    }
}
