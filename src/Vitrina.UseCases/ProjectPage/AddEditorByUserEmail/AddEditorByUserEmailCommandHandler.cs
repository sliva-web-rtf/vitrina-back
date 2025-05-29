using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.AddEditorByUserEmail;

/// <inheritdoc />
public class AddEditorByUserEmailCommandHandler(
    UserManager<Domain.User.User> userManager,
    IPageEditorRepository editorRepository,
    IProjectPageRepository pageRepository,
    IMapper mapper)
    : IRequestHandler<AddEditorByUserEmailCommand, PageEditorDto>
{
    /// <inheritdoc />
    public async Task<PageEditorDto> Handle(AddEditorByUserEmailCommand request, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetByIdAsync(request.PageId, cancellationToken);
        page.ThrowExceptionIfNoAccessRights(request.IdAuthorizedUser);
        if (page.Editors.Any(editor => editor.User.Email == request.UserEmail.Email))
        {
            throw new DomainException($"The editor with email = {request.UserEmail.Email} has already been added.");
        }

        var user = await userManager.FindByEmailAsync(request.UserEmail.Email) ??
                   throw new NotFoundException(
                       $"User with email = {request.UserEmail.Email} does not exist on the platform");

        var editor = new PageEditor
        {
            PageId = request.PageId, UserId = user.Id, Status = EditorStatus.Editor, Id = Guid.NewGuid()
        };

        await editorRepository.AddAsync(editor, cancellationToken);
        await editorRepository.SaveChangesAsync(cancellationToken);
        return mapper.Map<PageEditorDto>(editor);
    }
}
