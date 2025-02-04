using MediatR;

namespace Vitrina.UseCases.Auth.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    public Task Handle(RegisterCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
}
