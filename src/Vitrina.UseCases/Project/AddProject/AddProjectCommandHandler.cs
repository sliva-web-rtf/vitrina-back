using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
/// Add project handler.
/// </summary>
internal class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, int>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public AddProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<int> Handle(AddProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<AddProjectCommand, Domain.Project.Project>(request);
        await dbContext.Projects.AddAsync(project, cancellationToken);
        var id = await dbContext.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}
