using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;

namespace Vitrina.UseCases.User.Auth.ChangePassword;

public class ChangePasswordCommandHandler(UserManager<Domain.User.User> userManager)
    : IRequestHandler<ChangePasswordCommand, ChangePasswordCommandResult>
{
    public async Task<ChangePasswordCommandResult> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email) ??
            throw new NotFoundException("User not found");

        var isCorrectPassword = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isCorrectPassword)
        {
            throw new UnauthorizedAccessException("Incorrect password");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        return new() { IsSuccess = true, Token = token };
    }
}
