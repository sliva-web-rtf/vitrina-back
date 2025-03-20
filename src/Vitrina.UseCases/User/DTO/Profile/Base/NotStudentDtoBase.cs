using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.User.DTO.Profile.Base;

public abstract class NotStudentDtoBase : UserDtoBase
{
    /// <summary>
    /// Place of work
    /// </summary>
    [StringLength(255, ErrorMessage = "The Company must be no more than 255 characters long.")]
    public string? Company { get; init; }

    /// <summary>
    /// Position in the company.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Post must be no more than 255 characters long.")]
    public string? Post { get; init; }
}
