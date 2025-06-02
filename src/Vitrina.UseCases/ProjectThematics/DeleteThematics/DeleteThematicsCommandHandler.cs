using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectThematics.DeleteThematics;

public class DeleteThematicsCommandHandler(IAppDbContext dbContext) : IRequestHandler<DeleteThematicsCommand>
{
    public async Task Handle(DeleteThematicsCommand request, CancellationToken cancellationToken)
    {
        var thematics = await dbContext.ProjectThematics.FindAsync(request.Id, cancellationToken)
                        ?? throw new NotFoundException(
                            $"The thematics with {nameof(request.Id)} = {request.Id} was not found");
        dbContext.ProjectThematics.Remove(thematics);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
