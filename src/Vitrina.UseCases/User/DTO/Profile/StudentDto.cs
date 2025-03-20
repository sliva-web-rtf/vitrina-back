namespace Vitrina.UseCases.User.DTO.Profile;

public class StudentDto : UpdateStudentDto
{
    /// <summary>
    /// Student projects.
    /// </summary>
    public ICollection<PreviewProjectDto> Projects { get; set; }
}
