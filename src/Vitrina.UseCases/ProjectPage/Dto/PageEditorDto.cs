using Vitrina.Domain.Project.Page.Editor;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto;

public record PageEditorDto : BaseEntityDto<Guid>
{
    required public ResponceShortenedUserDto User { get; init; }

    required public EditorStatus Status { get; init; }
}
