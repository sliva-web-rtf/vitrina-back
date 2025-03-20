using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Common.DTO.DeleteSpecialization;

/// <inheritdoc />
public class DeleteSpecializationCommandHandler(ISpecializationRepository repository, IMapper mapper)
    : IRequestHandler<DeleteSpecializationCommand, SpecializationDto>
{
    /// <inheritdoc />
    public async Task<SpecializationDto> Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
    {
        var specialization = await repository.Delete(request.Id, cancellationToken);
        return mapper.Map<SpecializationDto>(specialization);
    }
}
