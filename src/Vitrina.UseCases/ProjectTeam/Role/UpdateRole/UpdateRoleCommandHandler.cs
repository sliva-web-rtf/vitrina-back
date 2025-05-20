using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Role.UpdateRole;

/// <inheritdoc />
public class UpdateRoleCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateRoleCommand, ResponceRoleDto>
{
    /// <inheritdoc />
    public async Task<ResponceRoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await dbContext.ProjectRoles.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Roles with id = {request.Id} not found");
        var roleDto = mapper.Map<RequestRoleDto>(role);
        request.PatchDocument.ApplyTo(roleDto);
        _ = await dbContext.ProjectRoles.FirstOrDefaultAsync(existingRole =>
                existingRole.Id != request.Id && existingRole.Name == roleDto.Name, cancellationToken)
            ?? throw new DomainException($"The role with {nameof(role.Name)} = {roleDto.Name} already exists");
        mapper.Map(roleDto, role);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ResponceRoleDto>(role);
    }
}
