using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectTeam.GetTeamById;

/// <inheritdoc />
public class GetTeamByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetTeamByIdQuery, ResponceTeamDto>
{
    /// <inheritdoc />
    public async Task<ResponceTeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await dbContext.Teams.FindAsync(request.Id)
                   ?? throw new NotFoundException($"The team with id = {request.Id} was not found");
        return mapper.Map<ResponceTeamDto>(team);
    }
}
