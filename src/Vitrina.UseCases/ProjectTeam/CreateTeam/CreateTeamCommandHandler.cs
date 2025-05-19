using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam.CreateTeam;

/// <inheritdoc />
public class CreateTeamCommandHandler(
    IAppDbContext dbContext,
    UserManager<Domain.User.User> userManager,
    IMapper mapper)
    : IRequestHandler<CreateTeamCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var teamDto = request.TeamDto;
        var project = await dbContext.Projects
                          .FirstOrDefaultAsync(project => project.Id == teamDto.ProjectId, cancellationToken)
                      ?? throw new NotFoundException(
                          $"Failed to find a project with the specified id = {teamDto.ProjectId}");
        project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        if (project.Team is not null)
        {
            throw new DomainException($"The project with the specified id = {project.Id} already has a team");
        }

        project.Team = await ConvertToTeamAsync(teamDto);
        await dbContext.SaveChangesAsync(cancellationToken);
        return project.Team.Id;
    }

    private async Task<Team> ConvertToTeamAsync(CreateTeamDto teamDto)
    {
        var team = new Team { Id = Guid.NewGuid(), Name = teamDto.Name, ProjectId = teamDto.ProjectId };
        foreach (var teammateDto in teamDto.TeamMembers)
        {
            var teammate = await GetTeammate(userManager, mapper, teammateDto);
            team.TeamMembers.Add(teammate);
        }

        return team;
    }

    public static async Task<Domain.Project.Teammate.Teammate> GetTeammate(
        UserManager<Domain.User.User> userManager,
        IMapper mapper,
        RequestTeammateDto teammateDto)
    {
        var user = await userManager.FindByEmailAsync(teammateDto.User.Email);
        if (user is null)
        {
            user = mapper.Map<Domain.User.User>(teammateDto.User);
            user.RoleOnPlatform = RoleOnPlatformEnum.Student;
        }

        if (user.FirstName != teammateDto.User.FirstName || user.LastName != teammateDto.User.LastName ||
            user.Patronymic != teammateDto.User.Patronymic)
        {
            throw new DomainException($"Incorrect full name of the user with email = {user.Email}");
        }

        var teammate = mapper.Map<Domain.Project.Teammate.Teammate>(teammateDto);
        teammate.User = user;
        return teammate;
    }
}
