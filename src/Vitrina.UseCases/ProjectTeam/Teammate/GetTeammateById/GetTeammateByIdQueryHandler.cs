using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.ProjectTeam.Teammate;
using Vitrina.UseCases.ProjectTeam.Teammate.GetTeammateById;

namespace Vitrina.UseCases.Project.Teammate.GetTeammateById;

/// <inheritdoc />
public class GetTeammateByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetTeammateByIdQuery, ResponceTeammateDto>
{
    /// <inheritdoc />
    public async Task<ResponceTeammateDto> Handle(GetTeammateByIdQuery request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException(
                           $"The team member with the specified id = {request.Id} was not found.");
        return mapper.Map<ResponceTeammateDto>(teammate);
    }
}
