using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPage.CreateProjectPage;

/// <inheritdoc />
public class CreateProjectPageCommandHandler(
    IMapper mapper,
    IProjectPageRepository repository,
    UserManager<Domain.User.User> userManager,
    ContentBlockDtoValidator validator)
    : IRequestHandler<CreateProjectPageCommand, Guid>
{
    /// <inheritdoc />
    public async Task<Guid> Handle(CreateProjectPageCommand request, CancellationToken cancellationToken)
    {
        var page = new Domain.Project.Page.ProjectPage { Id = Guid.NewGuid(), ReadyStatus = PageReadyStatusEnum.Draft };
        var creator = await userManager.FindByIdAsync($"{request.IdAuthorizedUser}");
        var editor = new PageEditor { Id = Guid.NewGuid(), UserId = creator.Id, PageId = page.Id };
        page.Editors.Add(editor);
        foreach (var blockDto in request.PageDto.ContentBlocks)
        {
            var validationResult = await validator.ValidateAsync(blockDto, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            page.ContentBlocks.Add(mapper.Map<ContentBlock>(blockDto));
        }

        page.NumberCustomBlocks();
        await repository.AddAsync(page, cancellationToken);
        try
        {
            await repository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
        }

        return page.Id;
    }
}
