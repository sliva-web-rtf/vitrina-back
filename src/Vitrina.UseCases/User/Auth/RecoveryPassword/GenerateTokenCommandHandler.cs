using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Enums.Email;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.User.Auth.RecoveryPassword;

public class GenerateTokenCommandHandler(
    UserManager<Domain.User.User> userManager,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<GenerateTokenCommand, GenerateTokenCommandResult>
{
    public async Task<GenerateTokenCommandResult> Handle(
        GenerateTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.IsEmailConfirmedAsync(user))
        {
            return new() { IsSuccess = true };
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var httpContextRequest = httpContextAccessor.HttpContext.Request;
        var baseUrl = $"{httpContextRequest.Scheme}://{httpContextRequest.Host}";
        var resetLink = $"{baseUrl}/api/auth/reset-password?token={token}&email={user.Email}";

        var result = await emailSender.SendEmailAsync(request.Email,
            resetLink,
            "Восстановление пароля");

        return new() { IsSuccess = result == EmailResult.Success };
    }
}
