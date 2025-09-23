using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetVerificationResult;

public class GetVerificationResultQueryHandler(IProjectPageRepository repository, UserManager<Domain.User.User> userManager, IAppDbContext dbContext)
    : IRequestHandler<GetVerificationResultQuery, VerificationResultDto>
{
    public async Task<VerificationResultDto> Handle(GetVerificationResultQuery request, CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.PageId, cancellationToken);
        var user = await userManager.FindByIdAsync($"{request.IdAuthorizedUser}");

        if (user?.RoleOnPlatform != RoleOnPlatformEnum.Administrator)
        {
            page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        }

        var result = await dbContext.VerificationResults.FirstOrDefaultAsync(res => res.PageId == request.PageId)
            ?? throw new NotFoundException("Результат проверки для этой страницы не найден");

        return new VerificationResultDto { NewPageStatus = result.NewPageStatus, Message = result.Message, };
    }
}
