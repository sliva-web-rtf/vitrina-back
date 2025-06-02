using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectThematics.GetThematicsById;

public class GetThematicsByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetThematicsByIdQuery, ResponceThematicsDto>
{
    public async Task<ResponceThematicsDto> Handle(GetThematicsByIdQuery request, CancellationToken cancellationToken)
    {
        var thematics = await dbContext.ProjectThematics.FindAsync(request.ThematicsId, cancellationToken)
                        ?? throw new NotFoundException(
                            $"The thematics with {nameof(request.ThematicsId)} = {request.ThematicsId} was not found");
        return mapper.Map<ResponceThematicsDto>(thematics);
    }
}
