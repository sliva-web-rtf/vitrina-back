using MediatR;

namespace Vitrina.UseCases.CodeSender;

public class SendConfirmationCodeCommand(string email, string confirmCode) : IRequest
{
    public string Email { get; init; } = email;

    public string ConfirmCode { get; init; } = confirmCode;
}
