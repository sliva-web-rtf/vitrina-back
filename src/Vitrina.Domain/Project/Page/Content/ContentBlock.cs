using System.ComponentModel.DataAnnotations.Schema;

namespace Vitrina.Domain.Project.Page;

public class ContentBlock : BaseEntity<Guid>
{
    required public Guid PageId { get; init; }

    required public ProjectPage Page { get; init; }

    [Column(TypeName = "jsonb")] required public string Content { get; set; }

    required public ContentTypeEnum ContentType { get; init; }
}
