using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
/// Handler.
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
        return await dbContext.Projects
            .ProjectTo<ProjectDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new ArgumentException($"Project with id {request.Id} not found.");
    }
}
