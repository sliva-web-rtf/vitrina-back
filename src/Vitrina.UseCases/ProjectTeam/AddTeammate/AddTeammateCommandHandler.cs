using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.ProjectTeam.CreateTeam;

namespace Vitrina.UseCases.ProjectTeam.AddTeammate;

/// <inheritdoc />
public class AddTeammateCommandHandler(
    IAppDbContext dbContext,
    UserManager<Domain.User.User> userManager,
    IMapper mapper)
    : IRequestHandler<AddTeammateCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(AddTeammateCommand request, CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FindAsync(request.TeamId, cancellationToken)
                   ?? throw new NotFoundException($"The team with id = {request.TeamId} was not found");
        team.Project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        var teammate = await CreateTeamCommandHandler.GetTeammate(userManager, mapper, request.TeammateDto);
        if (team.TeamMembers.Any(currentTeammate => currentTeammate.UserId == teammate.UserId))
        {
            throw new DomainException("The team member with the transmitted data is already in the team.");
        }

        team.TeamMembers.Add(teammate);
        await dbContext.SaveChangesAsync(cancellationToken);
        return teammate.Id;
    }
}
