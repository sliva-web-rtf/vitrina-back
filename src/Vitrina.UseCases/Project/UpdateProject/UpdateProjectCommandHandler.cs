using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.UpdateProject;

internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public UpdateProjectCommandHandler(IMapper mapper, IAppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var project = await appDbContext
                .Projects
                .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
                        ?? throw new DomainException("Project not found");
            var users = await appDbContext.Teammates
                .Include(u => u.Roles)
                .Where(u => u.ProjectId == project.Id)
                .ToListAsync(cancellationToken);
            var allRoles = await appDbContext.ProjectRoles.ToListAsync(cancellationToken);
            mapper.Map(request.Project, project);
            var resultUser = new List<Teammate>();
            foreach (var user in request.Project.Users)
            {
                var userFromDb = users.FirstOrDefault(u => u.Id == user.Id);
                mapper.Map(user, userFromDb);
                userFromDb ??= mapper.Map<Teammate>(user);
                userFromDb.Roles.Clear();
                foreach (var role in user.Roles)
                {
                    var userRoleFromDb = userFromDb.Roles.FirstOrDefault(r => r.Name == role.Name);
                    if (userRoleFromDb == null)
                    {
                        var roleFromDb = allRoles.FirstOrDefault(r => r.Name == role.Name);
                        if (roleFromDb != null)
                        {
                            userFromDb.Roles.Add(roleFromDb);
                        }
                        else
                        {
                            var newRole = new ProjectRole { Name = role.Name };
                            userFromDb.Roles.Add(newRole);
                        }
                    }
                }

                resultUser.Add(userFromDb);
            }
            project.Users = resultUser;
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new DomainException(ex.Message, ex.InnerException);
        }
    }
}
