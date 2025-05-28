using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.User.GetUserProjects;

/// <inheritdoc />
public class GetUserProjectsByUserIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetUserProjectsByUserIdQuery, ICollection<CreateProjectDto>>
{
    public async Task<ICollection<CreateProjectDto>> Handle(GetUserProjectsByUserIdQuery request,
        CancellationToken cancellationToken) =>
        await dbContext.Teammates
            .Where(teammate => teammate.UserId == request.UserId)
            .Select(teammate => mapper.Map<CreateProjectDto>(teammate.Team))
            .ToListAsync(cancellationToken);
}
