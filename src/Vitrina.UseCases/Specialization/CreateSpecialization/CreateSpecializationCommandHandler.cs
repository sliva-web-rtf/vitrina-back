using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Specialization.CreateSpecialization;

/// <inheritdoc />
public class CreateSpecializationCommandHandler(ISpecializationRepository repository, IMapper mapper)
    : IRequestHandler<CreateSpecializationCommand, SpecializationDto>
{
    /// <inheritdoc />
    public async Task<SpecializationDto> Handle(CreateSpecializationCommand request,
        CancellationToken cancellationToken)
    {
        var specialization = await repository.Create(request.Name, cancellationToken);
        return mapper.Map<SpecializationDto>(specialization);
    }
}
