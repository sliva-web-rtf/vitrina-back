using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public class GetUserProjectsByUserIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserProjectsByUserIdQuery, ICollection<ProjectDto>>
{
    public async Task<ICollection<ProjectDto>> Handle(GetUserProjectsByUserIdQuery request,
        CancellationToken cancellationToken) =>
        await dbContext.Teammates
            .Where(teammate => teammate.UserId == request.UserId)
            .Select(teammate => mapper.Map<ProjectDto>(teammate.Project))
            .ToListAsync(cancellationToken);
}
