using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.UpdateProject;

internal class UpdateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<UpdateProjectCommand, ResponceProjectDto>
{
    public async Task<ResponceProjectDto> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.FindAsync(request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException($"Project with id = {request.ProjectId} not found.");
        project.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        var projectDto = mapper.Map<UpdateProjectDto>(project);
        request.PatchDocument.ApplyTo(projectDto);
        mapper.Map(projectDto, project);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ResponceProjectDto>(project);
    }
}
