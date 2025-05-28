using Vitrina.UseCases.Project;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.ProjectTeam.Teammate;

public record RequestTeammateDto : TeammateDtoBase
{
    public RequestShortenedUserDto User { get; init; }
}
