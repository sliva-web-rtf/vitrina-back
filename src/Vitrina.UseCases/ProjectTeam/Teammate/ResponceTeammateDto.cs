using Vitrina.UseCases.Project;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.ProjectTeam.Teammate;

public record ResponceTeammateDto : TeammateDtoBase
{
    public Guid Id { get; init; }

    required public ResponceShortenedUserDto User { get; init; }
}
