using Vitrina.Domain.Enums.Email;

namespace Vitrina.Infrastructure.Abstractions.Interfaces;

public interface IEmailSender
{
    public Task<EmailResult> SendEmailAsync(
        string emailAddress,
        string message,
        string subject);
}
