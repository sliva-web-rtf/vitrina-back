using MediatR;

namespace Vitrina.UseCases.ProjectPage.SendingNotifications;

public record SendingNotificationsCommand(Guid PageId) : IRequest;
