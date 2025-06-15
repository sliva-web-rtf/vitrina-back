using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.User.Auth.ForgotPassword;

public class ForgotPasswordCommandHandler(
    UserManager<Domain.User.User> userManager,
    IEmailSender emailSender,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<ForgotPasswordCommand, ForgotPasswordCommandResult>
{
    public async Task<ForgotPasswordCommandResult> Handle(
        ForgotPasswordCommand request,
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

        await emailSender.SendEmailAsync(request.Email,
            resetLink,
            "Восстановление пароля");

        return new() { IsSuccess = true };
    }
}
