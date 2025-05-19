using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.UpdateTeam;

/// <inheritdoc />
public class UpdateTeamCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateTeamCommand, ResponceTeamDto>
{
    /// <inheritdoc />
    public async Task<ResponceTeamDto> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"The team with id = {request.Id} was not found");
        team.Project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        var teamDto = mapper.Map<UpdateTeamDto>(team);
        request.PatchDocument.ApplyTo(teamDto);
        mapper.Map(teamDto, team);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ResponceTeamDto>(teamDto);
    }
}
