using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.UserSpecialization.CreateSpecialization;

/// <summary>
///     The specialization creation command.
/// </summary>
public record CreateSpecializationCommand : IRequest<SpecializationDto>
{
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")]
    public string Name { get; init; }
}
