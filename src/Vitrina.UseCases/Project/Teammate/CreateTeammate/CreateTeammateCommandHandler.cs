using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.Teammate.CreateTeammate;

/// <inheritdoc />
public class CreateTeammateCommandHandler(
    IAppDbContext dbContext,
    UserManager<Domain.User.User> userManager,
    IMapper mapper)
    : IRequestHandler<CreateTeammateCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateTeammateCommand request, CancellationToken cancellationToken)
    {
        var teammateDto = request.TeammateDto;
        var project =
            await dbContext.Projects.FirstOrDefaultAsync(project => project.Id == teammateDto.ProjectId,
                cancellationToken)
            ?? throw new NotFoundException($"Failed to find a project with the specified id = {teammateDto.ProjectId}");
        if (project.CheckYourEditingRights(request.IdAuthorizedUser))
        {
            throw new ForbiddenException("You do not have the rights to change the data of this project.");
        }

        var user = await userManager.FindByEmailAsync(teammateDto.User.Email);
        if (user != null && (user.FirstName != teammateDto.User.FirstName ||
                             user.LastName != teammateDto.User.LastName ||
                             user.Patronymic != teammateDto.User.Patronymic))
        {
            throw new DomainException("You do not have the rights to change the data of this project.");
        }

        var teammate = mapper.Map<Domain.Project.Teammate.Teammate>(teammateDto);
        await AddNewTeammateRolesAsync(dbContext, teammate, cancellationToken);
        dbContext.Teammates.Add(teammate);
        await dbContext.SaveChangesAsync(cancellationToken);
        return teammate.Id;
    }

    public static async Task AddNewTeammateRolesAsync(IAppDbContext dbContext,
        Domain.Project.Teammate.Teammate teammate, CancellationToken cancellationToken)
    {
        var updatedRoles = new List<ProjectRole>();
        var allRoles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);

        foreach (var role in teammate.Roles)
        {
            var existingRole = allRoles.FirstOrDefault(projectRole =>
                projectRole.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));

            if (existingRole != null)
            {
                updatedRoles.Add(existingRole);
            }
            else
            {
                var newRole = new ProjectRole { Name = role.Name };
                dbContext.ProjectRoles.Add(newRole);
                allRoles.Add(newRole);
                updatedRoles.Add(newRole);
            }
        }

        teammate.Roles = updatedRoles;
    }
}
