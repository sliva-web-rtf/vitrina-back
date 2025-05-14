using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class CreateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<CreateProjectCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<CreateProjectCommand, Domain.Project.Project>(request);

        // Понадобится если будем указывать членов команды при публикации проекта
        // TODO: Добавить несуществующих пользователей со статусом регистрации - не зарегистрирован и найти в системе существующих
        // await AddNewUserRoles(project, cancellationToken);

        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

    private async Task AddNewUserRoles(Domain.Project.Project project, CancellationToken cancellationToken)
    {
        var allRoles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);

        foreach (var teammate in project.TeamMembers)
        {
            var updatedRoles = new List<ProjectRole>();

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
}
