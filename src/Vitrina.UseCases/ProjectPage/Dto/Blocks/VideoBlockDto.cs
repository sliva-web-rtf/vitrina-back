using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record VideoBlockDto : NumberedBlockBaseDto
{
    required public FileDto Video { get; init; }
}
