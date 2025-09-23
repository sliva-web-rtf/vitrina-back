using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectPage.SendingNotifications;

public class SendingNotificationsCommandHandler(IEmailSender emailSender, IAppDbContext dbContext)
    : IRequestHandler<SendingNotificationsCommand>
{
    private const string Rejection = "Страница была отправлена на доработку.";
    private const string Approval = "Страница прошла верификацию и была опубликована.";

    public async Task Handle(SendingNotificationsCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await dbContext.VerificationResults.FirstOrDefaultAsync(validationResult => validationResult.PageId == request.PageId) ??
                               throw new NotFoundException($"Изменения статуса готовности страницы с ID = {request.PageId} не найдено");

        foreach (var email in dbContext.PageEditors
                     .Where(pageEditor => pageEditor.PageId == request.PageId)
                     .Select(pageEditor => pageEditor.User.Email))
        {
            var startMessage = validationResult.NewPageStatus == PageReadyStatusEnum.Rejected ? Rejection : Approval;
            await emailSender.SendEmailAsync(
                email,
                $"{startMessage}\n Комментарий модератора:\n{validationResult.Message}",
                "Измененние статуса страницы"
            );
        }
    }
}
