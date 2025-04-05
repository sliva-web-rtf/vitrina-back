namespace Vitrina.UseCases.User.DTO.Profile;

public interface IHavingProjects
{
    public ICollection<PreviewProjectDto> Projects { get; set; }
}
