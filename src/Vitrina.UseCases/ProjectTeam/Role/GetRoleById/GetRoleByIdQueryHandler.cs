using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.Role.GetRoleById;

/// <inheritdoc />
public class GetRoleByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetRoleByIdQuery, ResponceRoleDto>
{
    /// <inheritdoc />
    public async Task<ResponceRoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await dbContext.ProjectRoles.FindAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"The role with {nameof(request.Id)} = {request.Id} was not found");
        return mapper.Map<ResponceRoleDto>(role);
    }
}
