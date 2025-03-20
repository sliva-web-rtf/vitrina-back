using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.Specialization.CreateSpecialization;

/// <summary>
/// The specialization creation command.
/// </summary>
public record CreateSpecializationCommand(
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")] string Name)
    : IRequest<SpecializationDto>;
