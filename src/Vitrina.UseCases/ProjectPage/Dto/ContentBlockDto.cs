using Newtonsoft.Json.Linq;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto;

public record ContentBlockDto : BaseEntityDto<Guid>
{
    required public JObject Content { get; init; }

    required public ContentTypeEnum ContentType { get; init; }
}
