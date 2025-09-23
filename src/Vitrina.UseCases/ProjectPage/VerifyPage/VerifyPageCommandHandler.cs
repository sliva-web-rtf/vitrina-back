using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPage.VerifyPage;

public class VerifyPageCommandHandler(IProjectPageRepository pageRepository, IAppDbContext dbContext)
    : IRequestHandler<VerifyPageCommand>
{
    /// <inheritdoc />
    public async Task Handle(VerifyPageCommand request, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetByIdAsync(request.PageId, cancellationToken);
        var verificationResult = new VerificationResult
        {
            Page = page,
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            PageId = request.PageId,
            NewPageStatus = request.VerificationDto.NewPageStatus,
            Message = request.VerificationDto.Message,
            ModeratorId = request.VerificationDto.ModeratorId,
        };

        var previousResult = await dbContext.VerificationResults.FirstOrDefaultAsync(result => result.PageId == request.PageId);
        if (previousResult != null)
        {
            dbContext.VerificationResults.Remove(previousResult);
        }
        await dbContext.VerificationResults.AddAsync(verificationResult, cancellationToken);
        page.ReadyStatus = request.VerificationDto.NewPageStatus;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
