using MediatR;

namespace Vitrina.UseCases.CodeSender;

public class SendConfirmationCodeCommand(string email, int confirmCode) : IRequest
{
    public string Email { get; init; } = email;

    public int ConfirmCode { get; init; } = confirmCode;
}
