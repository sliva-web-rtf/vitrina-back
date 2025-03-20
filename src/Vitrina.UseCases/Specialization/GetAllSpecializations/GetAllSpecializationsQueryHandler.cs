using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.User.Specialization;

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
