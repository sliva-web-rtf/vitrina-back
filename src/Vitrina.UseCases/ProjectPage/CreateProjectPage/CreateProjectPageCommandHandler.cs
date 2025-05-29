using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPage.CreateProjectPage;

/// <inheritdoc />
public class CreateProjectPageCommandHandler(
    IMapper mapper,
    IProjectPageRepository repository,
    UserManager<Domain.User.User> userManager)
    : IRequestHandler<CreateProjectPageCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = new Domain.Project.Page.ProjectPage { Id = Guid.NewGuid(), ReadyStatus = PageReadyStatusEnum.Draft };
        var creator = await userManager.FindByIdAsync($"{request.IdAuthorizedUser}");
        page.Editors.Add(mapper.Map<PageEditor>(creator));
        foreach (var blockDto in request.PageDto.ContentBlocks)
        {
            page.ContentBlocks.Add(mapper.Map<ContentBlock>(blockDto));
        }

        page.NumberCustomBlocks();
        await repository.AddAsync(page, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
        return page.Id;
    }
}
