using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam.GetTeammates;

/// <inheritdoc />
public class GetTeammatesQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetTeammatesQuery, ICollection<ResponceTeammateDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<ResponceTeammateDto>> Handle(GetTeammatesQuery request,
        CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException();
        return mapper.Map<ICollection<ResponceTeammateDto>>(team.TeamMembers);
    }
}
