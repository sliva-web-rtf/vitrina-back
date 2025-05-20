using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Role.GetRoles;

/// <inheritdoc />
public class GetRolesQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetRolesQuery, ICollection<ResponceRoleDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<ResponceRoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await dbContext.ProjectRoles.ToListAsync(cancellationToken);
        return mapper.Map<ICollection<ResponceRoleDto>>(roles);
    }
}
