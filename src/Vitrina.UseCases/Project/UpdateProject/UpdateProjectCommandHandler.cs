using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.UpdateProject;

internal class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public UpdateProjectCommandHandler(IMapper mapper, IAppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.appDbContext = appDbContext;
    }

    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
            ?? throw new DomainException("Project not found");
        mapper.Map(request.Project, project);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
