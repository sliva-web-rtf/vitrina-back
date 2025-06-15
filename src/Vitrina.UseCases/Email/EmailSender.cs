using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using Vitrina.Domain.Enums.Email;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Vitrina.UseCases.Email;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> logger;
    private readonly EmailSettings emailSettings;

    public EmailSender(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailSender> logger)
    {
        this.logger = logger;
        this.emailSettings = emailSettings.Value;
    }

    public async Task<EmailResult> SendEmailAsync(
        string emailAddress,
        string message,
        string subject)
    {
        var letter = new MimeMessage();
        letter.From.Add(MailboxAddress.Parse(emailSettings.FromAddress));
        letter.To.Add(MailboxAddress.Parse(emailAddress));
        letter.Subject = subject;
        letter.Body = new TextPart(TextFormat.Plain) { Text = message };

        try
        {
            using var smtp = new SmtpClient();
            logger.Log(LogLevel.Information, "Connecting to smtp");
            await smtp.ConnectAsync(emailSettings.Host, 587, SecureSocketOptions.StartTls);
            logger.Log(LogLevel.Information, "Authenticating to smtp");
            await smtp.AuthenticateAsync(emailSettings.Username, emailSettings.Password);
            logger.Log(LogLevel.Information, "Sending email to smtp");
            await smtp.SendAsync(letter);
            logger.Log(LogLevel.Information, "Disconnecting from smtp");
            await smtp.DisconnectAsync(true);

            return EmailResult.Success;
        }
        catch
        {
            return EmailResult.Failure;
        }
        finally
        {
            letter.Dispose();
        }
    }
}
