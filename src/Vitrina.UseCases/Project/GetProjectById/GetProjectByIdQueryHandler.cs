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
internal class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public GetProjectByIdQueryHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects
                          .Include(project => project.Contents)
                          .Include(project => project.Tags)
                          .Include(project => project.Users)
                          .Include(project => project.Users)
                          .Include(project => project.CustomBlocks)
                          .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
                      ?? throw new NotFoundException($"Project with id {request.Id} not found.");
        project.CustomBlocks = project.CustomBlocks
            .OrderBy(block => block.SequenceNumber)
            .ToList();
        var projectDto = mapper.Map<ProjectDto>(project);
        foreach (var content in projectDto.Contents)
        {
            content.ImageUrl = content.ImageUrl.Split("/").Last();
        }

        return projectDto;
    }
}
