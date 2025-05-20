using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Teammate;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Role.CreateRole;

/// <inheritdoc />
public class CreateRoleCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateRoleCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleDto = request.RoleDto;
        _ = await dbContext.ProjectRoles.FirstOrDefaultAsync(existingRole => existingRole.Name == roleDto.Name,
                cancellationToken)
            ?? throw new DomainException($"The role with {nameof(roleDto.Name)} = {roleDto.Name} already exists");
        var role = mapper.Map<ProjectRole>(roleDto);
        dbContext.ProjectRoles.Add(role);
        await dbContext.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
