using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Specialization.GetAllSpecializations;

/// <inheritdoc />
public class GetAllSpecializationsQueryHandler(ISpecializationRepository repository, IMapper mapper)
    : IRequestHandler<GetAllSpecializationsQuery, SpecializationDto[]>
{
    /// <inheritdoc />
    public async Task<SpecializationDto[]> Handle(GetAllSpecializationsQuery request, CancellationToken cancellationToken)
    {
        var specializations = await repository.GetAll(cancellationToken);
        return mapper.Map<IEnumerable<SpecializationDto>>(specializations).ToArray();
    }
}
