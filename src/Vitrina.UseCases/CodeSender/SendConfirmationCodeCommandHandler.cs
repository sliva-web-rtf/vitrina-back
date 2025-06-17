using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.CodeSender;

public class SendConfirmationCodeCommandHandler(IEmailSender sender)
    : IRequestHandler<SendConfirmationCodeCommand>
{
    public async Task Handle(SendConfirmationCodeCommand request, CancellationToken cancellationToken)
    {
        await sender.SendEmailAsync(request.Email, $"Ваш код для подтверждения регистрации: {request.ConfirmCode}",
            "Ваш код");
    }
}
