using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Auth.ConfirmEmail;

/// <summary>
/// Handler for <see cref="ConfirmEmailCommand"/>
/// </summary>
public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ConfirmEmailCommandResult>
{
    private readonly IAppDbContext appDbContext;
    private readonly SignInManager<User> signInManager;
    private readonly IAuthenticationTokenService tokenService;

    /// <summary>
    /// Constructor.
    /// </summary>
    public ConfirmEmailCommandHandler(IAppDbContext appDbContext, SignInManager<User> signInManager, IAuthenticationTokenService tokenService)
    {
        this.appDbContext = appDbContext;
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<ConfirmEmailCommandResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var exist = await appDbContext.Codes
            .AnyAsync(c => c.Code == request.ConfirmationCode && c.UserId == request.UserId,
                cancellationToken);
        if (!exist)
        {
            return new ConfirmEmailCommandResult { IsSuccess = false, Message = "wrong confirmation code." };
        }

        var user = await appDbContext.Users.FirstAsync(u => u.Id == request.UserId, cancellationToken);
        user.EmailConfirmed = true;
        var principal = await signInManager.CreateUserPrincipalAsync(user);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return new ConfirmEmailCommandResult()
        {
            IsSuccess = true, UserId = user.Id, Token = TokenModelGenerator.Generate(tokenService, principal.Claims)
        };
    }
}
