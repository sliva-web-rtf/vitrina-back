using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectSphere.DeleteSphere;

public class DeleteSphereCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteSphereCommand>
{
    public async Task Handle(DeleteSphereCommand request, CancellationToken cancellationToken)
    {
        var sphere = await dbContext.ProjectSpheres.FindAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException(
                         $"The sphere with {nameof(request.Id)} = {request.Id} was not found");
        dbContext.ProjectSpheres.Remove(sphere);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
