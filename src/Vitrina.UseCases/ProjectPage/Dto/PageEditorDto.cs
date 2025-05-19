using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPages;

public record PageEditorDto : BaseEntityDto<Guid>
{
    required public UserDtoBase User { get; init; }

    required public EditorStatus Status { get; init; }
}
