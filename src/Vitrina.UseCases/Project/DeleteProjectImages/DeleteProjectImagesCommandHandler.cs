using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.DeleteProjectImages;

internal class DeleteProjectImagesCommandHandler(IAppDbContext appDbContext, IHostingEnvironment hostingEnvironment)
    : IRequestHandler<DeleteProjectImagesCommand>
{
    public async Task Handle(DeleteProjectImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects
                          .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken)
                      ?? throw new NotFoundException();

        // TODO: реализовать удаление файлов боков страниц проектов из облака
    }
}
