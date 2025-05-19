using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.Teammate.GetTeammateById;

/// <inheritdoc />
public class GetTeammateByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetTeammateByIdQuery, TeammateDto>
{
    /// <inheritdoc />
    public async Task<TeammateDto> Handle(GetTeammateByIdQuery request, CancellationToken cancellationToken)
    {
        var teammate = await dbContext.Teammates.FindAsync(request.Id, cancellationToken)
                       ?? throw new NotFoundException(
                           $"The team member with the specified id = {request.Id} was not found.");
        return mapper.Map<TeammateDto>(teammate);
    }
}
