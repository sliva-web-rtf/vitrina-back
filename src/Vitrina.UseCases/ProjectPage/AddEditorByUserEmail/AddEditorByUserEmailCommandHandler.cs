using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.ProjectPages.AddEditorByUserEmail;

public class AddEditorByUserEmailCommandHandler(
    IUserRepository userRepository,
    IPageEditorRepository editorRepository,
    IProjectPageRepository pageRepository,
    IMapper mapper)
    : IRequestHandler<AddEditorByUserEmailCommand, PageEditorDto>
{
    public async Task<PageEditorDto> Handle(AddEditorByUserEmailCommand request, CancellationToken cancellationToken)
    {
        var page = await pageRepository.GetByIdAsync(request.PageId, cancellationToken);
        if (page.Editors.Any(editor => editor.User.Email == request.UserEmail.Email))
        {
            throw new DomainException($"The editor with email = {request.UserEmail.Email} has already been added.");
        }
        var user = await userRepository.FindByEmailAsync(request.UserEmail.Email, cancellationToken);
        var editor = new PageEditor
        {
            PageId = request.PageId,
            UserId = user.Id,
            Status = EditorStatus.Editor,
            Id = Guid.NewGuid(),
        };

        await editorRepository.AddAsync(editor, cancellationToken);
        await editorRepository.SaveChangesAsync(cancellationToken);
        return mapper.Map<PageEditorDto>(editor);
    }
}
