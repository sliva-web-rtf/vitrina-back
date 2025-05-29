using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.GetProjectPageEditors;

/// <inheritdoc />
public class GetProjectPageEditorsQueryHandler(
    IProjectPageRepository repository,
    IMapper mapper,
    UserManager<Domain.User.User> userManager)
    : IRequestHandler<GetProjectPageEditorsQuery, ICollection<PageEditorDto>>
{
    /// <inheritdoc />
    public async Task<ICollection<PageEditorDto>> Handle(GetProjectPageEditorsQuery request,
        CancellationToken cancellationToken)
    {
        var page = await repository.GetByIdAsync(request.PageId, cancellationToken);
        if (page.ReadyStatus != PageReadyStatusEnum.Published)
        {
            var user = await userManager.FindByIdAsync($"{request.IdAuthorizedUser}");
            if (user?.RoleOnPlatform != RoleOnPlatformEnum.Administrator)
            {
                page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
            }
        }

        return mapper.Map<ICollection<PageEditorDto>>(page.Editors);
    }
}
