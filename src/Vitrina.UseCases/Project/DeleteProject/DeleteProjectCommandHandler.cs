using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.DeleteProject;

internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly IHostingEnvironment hostingEnvironment;

    public DeleteProjectCommandHandler(IAppDbContext appDbContext, IHostingEnvironment hostingEnvironment)
    {
        this.appDbContext = appDbContext;
        this.hostingEnvironment = hostingEnvironment;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects.Include(p => p.Contents)
                          .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException();
        foreach (var content in project.Contents)
        {
            var webRootDirectory = hostingEnvironment.WebRootPath.TrimEnd('/');
            var name = content.ImageUrl.Split("/").Last();
            var path = $"/Avatars/{name}";
            var pathToFile = webRootDirectory + path;
            File.Delete(pathToFile);
        }

        appDbContext.Projects.Remove(project);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
