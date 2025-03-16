using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.Common;

public class StudentDto : UpdateStudentDto
{
    /// <summary>
    /// Student projects.
    /// </summary>
    public ICollection<PreviewProjectDto> Projects { get; init; }
}
