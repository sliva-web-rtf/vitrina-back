using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.UserSpecialization;

/// <summary>
///     User specialization.
/// </summary>
public record SpecializationDto
{
    /// <summary>
    ///     ID of the specialization.
    /// </summary>
    required public Guid Id { get; set; }

    /// <summary>
    ///     Name of the specialization.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    required public string Name { get; init; }
}
