using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class CreateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<CreateProjectCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // TODO: переписать полностью.
        try
        {
            var project = mapper.Map<CreateProjectCommand, Domain.Project.Project>(request);
            await AddNewUserRoles(project, cancellationToken);
            await dbContext.Projects.AddAsync(project, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
        catch (Exception ex)
        {
            throw new DomainException("An error occurred while creating the project.", ex);
        }
    }

    private async Task AddNewUserRoles(Domain.Project.Project project, CancellationToken cancellationToken)
    {
        var allRoles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);

        foreach (var userInProject in project.Users)
        {
            var updatedRoles = new List<ProjectRole>();

            foreach (var role in userInProject.Roles)
            {
                var existingRole = allRoles
                    .FirstOrDefault(projectRole =>
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

            userInProject.Roles = updatedRoles;
        }
    }
}
