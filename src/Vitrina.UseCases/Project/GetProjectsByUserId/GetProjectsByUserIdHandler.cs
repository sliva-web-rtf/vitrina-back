using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.GetProjectsByUserId;

public class GetProjectsByUserIdHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<GetProjectsByUserIdQuery, IEnumerable<ProjectDto>>
{
    public Task<IEnumerable<ProjectDto>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<ProjectDto>>(dbContext.Teammates
            .Where(teammate => teammate.UserId == request.UserId)
            .Select(teammate => mapper.Map<ProjectDto>(teammate.Project)));
    }
}
