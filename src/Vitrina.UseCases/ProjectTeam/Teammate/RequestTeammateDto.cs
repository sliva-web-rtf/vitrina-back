using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project;

namespace Vitrina.UseCases.ProjectTeam.Teammate;

public record RequestTeammateDto : TeammateDtoBase
{
    public RequestShortenedUserDto User { get; init; }
}
