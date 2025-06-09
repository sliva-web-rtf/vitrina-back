using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetProjectPage;

/// <inheritdoc />
public class GetProjectPageByIdQueryHandler(
    IProjectPageRepository repository,
    IMapper mapper,
    UserManager<Domain.User.User> userManager)
    : IRequestHandler<GetProjectPageByIdQuery, ResponceProjectPageDto>
{
    /// <inheritdoc />
    public async Task<ResponceProjectPageDto> Handle(GetProjectPageByIdQuery request,
        CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (page.ReadyStatus != PageReadyStatusEnum.Published)
        {
            var user = await userManager.FindByIdAsync($"{request.IdAuthorizedUser}");
            if (user?.RoleOnPlatform != RoleOnPlatformEnum.Administrator)
            {
                page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
            }
        }

        page.SortContentBlocks();
        var p = mapper.Map<ResponceProjectPageDto>(page);
        return p;
    }
}
