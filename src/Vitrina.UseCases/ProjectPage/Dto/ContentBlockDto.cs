using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPages;

public record ContentBlockDto : BaseEntityDto<Guid>
{
    public string Content { get; init; }

    public ContentTypeEnum ContentType { get; init; }
}
