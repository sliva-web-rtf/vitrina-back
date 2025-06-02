using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectThematics.GetThematics;

public class GetAllThematicsQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllThematicsQuery, ICollection<ResponceThematicsDto>>
{
    public async Task<ICollection<ResponceThematicsDto>> Handle(GetAllThematicsQuery request,
        CancellationToken cancellationToken)
    {
        var thematics = await dbContext.ProjectThematics.ToListAsync(cancellationToken);
        return mapper.Map<ICollection<ResponceThematicsDto>>(thematics);
    }
}
