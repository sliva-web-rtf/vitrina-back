using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public class GetUserProjectsByUserIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserProjectsByUserIdQuery, ICollection<PreviewProjectDto>>
{
    public async Task<ICollection<PreviewProjectDto>> Handle(GetUserProjectsByUserIdQuery request,
        CancellationToken cancellationToken) =>
        await dbContext.Teammates
            .Where(teammate => teammate.UserId == request.UserId)
            .Select(teammate => mapper.Map<PreviewProjectDto>(teammate.Project))
            .ToListAsync(cancellationToken);
}
