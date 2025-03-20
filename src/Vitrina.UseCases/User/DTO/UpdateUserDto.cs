using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.DTO;

/// <summary>
/// Dto for updating user data.
/// </summary>
public class UpdateUserDto : UpdateStudentDto
{
    /// <summary>
    /// Place of work
    /// </summary>
    public string? Post { get; init; }

    /// <summary>
    /// Position in the company.
    /// </summary>
    public string? Company { get; init; }
}
