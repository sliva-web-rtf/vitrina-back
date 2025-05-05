using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, int>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public AddProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<int> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = mapper.Map<AddProjectCommand, Domain.Project.Project>(request);

            var allRoles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);

            foreach (var userInProject in project.Users)
            {
                var updatedRoles = new List<ProjectRole>();

                foreach (var role in userInProject.Roles)
                {
                    var existingRole =
                        allRoles.FirstOrDefault(r => r.Name.Equals(role.Name, StringComparison.OrdinalIgnoreCase));

                    if (existingRole != null)
                    {
                        updatedRoles.Add(existingRole);
                    }
                    else
                    {
                        var newRole = new ProjectRole { Name = role.Name };

                        dbContext.ProjectRoles.Add(newRole);

                        await dbContext.SaveChangesAsync(cancellationToken);

                        allRoles.Add(newRole);

                        updatedRoles.Add(newRole);
                    }
                }

                userInProject.Roles = updatedRoles;
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            await dbContext.Projects.AddAsync(project, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
        catch (Exception ex)
        {
            throw new DomainException("An error occurred while creating the project.", ex);
        }
    }
}
