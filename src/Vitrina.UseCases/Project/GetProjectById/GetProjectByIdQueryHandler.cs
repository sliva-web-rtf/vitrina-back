using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
///     Handler.
/// </summary>
internal class GetProjectByIdQueryHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
                          .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                      ?? throw new NotFoundException($"Project with id {request.Id} not found.");
        project.Page.SortContentBlocks();
        var projectDto = mapper.Map<ProjectDto>(project);
        return projectDto;
    }
}
