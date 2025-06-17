using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;

namespace Vitrina.UseCases.User.Auth.ResetPassword;

public class ResetPasswordCommandHandler(UserManager<Domain.User.User> userManager)
    : IRequestHandler<ResetPasswordCommand, ResetPasswordCommandResult>
{
    public async Task<ResetPasswordCommandResult> Handle(
        ResetPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email) ??
            throw new NotFoundException("User not found");

        var result = await userManager.ResetPasswordAsync(user, request.Token, request.Password);

        return new() { IsSuccess = result.Succeeded, UserId = user.Id };
    }
}
