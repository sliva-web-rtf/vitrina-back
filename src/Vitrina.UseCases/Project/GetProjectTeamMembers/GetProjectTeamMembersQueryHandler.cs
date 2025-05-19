using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetProjectTeamMembers;

/// <inheritdoc />
public class GetProjectTeamMembersQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetProjectTeamMembersQuery, ICollection<TeammateDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<TeammateDto>> Handle(GetProjectTeamMembersQuery request,
        CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.FindAsync(request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException(
                          $"The project with the specified id = {request.ProjectId} was not found.");
        return mapper.Map<ICollection<TeammateDto>>(project.TeamMembers);
    }
}
