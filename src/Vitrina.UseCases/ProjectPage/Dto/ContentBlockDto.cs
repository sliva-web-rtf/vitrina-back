using Vitrina.Domain.Project.Page.Content;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto;

public record ContentBlockDto : BaseEntityDto<Guid>
{
    public string Content { get; init; }

    public ContentTypeEnum ContentType { get; init; }
}
