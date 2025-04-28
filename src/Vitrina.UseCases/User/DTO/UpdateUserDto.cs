using System.ComponentModel.DataAnnotations;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.DTO;

/// <summary>
///     Dto for updating user data.
/// </summary>
public class UpdateUserDto : UpdateStudentDto
{
    /// <summary>
    ///     Place of work
    /// </summary>
    [StringLength(255, ErrorMessage = "The Post must be no more than 255 characters long.")]
    public string? Post { get; init; }

    /// <summary>
    ///     Position in the company.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Company must be no more than 255 characters long.")]
    public string? Company { get; init; }
}
