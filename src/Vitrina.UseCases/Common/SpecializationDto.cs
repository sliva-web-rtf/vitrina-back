using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common;

/// <summary>
/// User specialization.
/// </summary>
public record SpecializationDto(
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")] string Name);
