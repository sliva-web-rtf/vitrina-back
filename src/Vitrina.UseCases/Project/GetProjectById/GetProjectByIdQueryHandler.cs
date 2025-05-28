using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
///     Handler.
/// </summary>
internal class GetProjectByIdQueryHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<GetProjectByIdQuery, ResponceProjectDto>
{
    public async Task<ResponceProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
                          .FirstOrDefaultAsync(existingProject => existingProject.Id == request.Id, cancellationToken)
                      ?? throw new NotFoundException($"Project with id {request.Id} not found.");
        return mapper.Map<ResponceProjectDto>(project);
    }
}
