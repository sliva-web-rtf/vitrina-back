using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.UserSpecialization.CreateSpecialization;

/// <inheritdoc />
public class CreateSpecializationCommandHandler(ISpecializationRepository repository)
    : IRequestHandler<CreateSpecializationCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateSpecializationCommand request,
        CancellationToken cancellationToken)
    {
        var specialization = await repository.Create(request.Name, cancellationToken);
        return specialization.Id;
    }
}
