using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.DeleteProjectImages;

internal class DeleteProjectImagesCommandHandler : IRequestHandler<DeleteProjectImagesCommand>
{
    private readonly IAppDbContext appDbContext;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

    public DeleteProjectImagesCommandHandler(IAppDbContext appDbContext, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
    {
        this.appDbContext = appDbContext;
        this.hostingEnvironment = hostingEnvironment;
    }

    public async Task Handle(DeleteProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects.Include(p => p.Contents).FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
            ?? throw new NotFoundException();
        foreach (var content in project.Contents)
        {
            var webRootDirectory = hostingEnvironment.WebRootPath.TrimEnd('/');
            var name = content.ImageUrl.Split("/").Last();
            var path = $"/Avatars/{name}";
            var pathToFile = webRootDirectory + path;
            File.Delete(pathToFile);
        }
        await appDbContext.Contents
            .Where(c => c.ProjectId == request.ProjectId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
