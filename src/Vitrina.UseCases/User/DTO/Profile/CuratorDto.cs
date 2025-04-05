using Vitrina.UseCases.User.DTO.Profile.Base;

namespace Vitrina.UseCases.User.DTO.Profile;

public class CuratorDto : NotStudentDtoBase, IHavingProjects
{
    /// <summary>
    /// User-curated projects.
    /// </summary>
    public ICollection<PreviewProjectDto> Projects { get; set; }
}
