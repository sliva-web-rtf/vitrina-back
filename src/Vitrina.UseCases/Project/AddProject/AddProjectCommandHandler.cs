using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
/// Add project handler.
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

    /// <inheritdoc/>
    public async Task<int> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = mapper.Map<AddProjectCommand, Domain.Project.Project>(request);
            var allRoles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);
            foreach (var userInProject in project.Users)
            {
                var clone = new List<ProjectRole>(userInProject.Roles);
                foreach (var t in clone)
                {
                    var role = allRoles.FirstOrDefault(r => r.Name.ToLower() == t.Name.ToLower());
                    if (role != null)
                    {
                        var userRole = userInProject.Roles.First(r => r.Name == t.Name);
                        userInProject.Roles.Remove(userRole);
                        userInProject.Roles.Add(role);
                    }
                }
            }
            await dbContext.Projects.AddAsync(project, cancellationToken);
            var id = await dbContext.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException);
        }
    }
}
