namespace Vitrina.UseCases.User.DTO.Profile;

public class StudentDto : UpdateStudentDto, IHavingProjects
{
    /// <summary>
    /// Student projects.
    /// </summary>
    public ICollection<PreviewProjectDto> Projects { get; set; }
}
