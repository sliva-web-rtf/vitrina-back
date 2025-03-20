using System.ComponentModel.DataAnnotations;
using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.User.Specialization.CreateSpecialization;

/// <summary>
/// The specialization creation command.
/// </summary>
public record CreateSpecializationCommand(
    [StringLength(255, ErrorMessage = "The Name must be no more than 255 characters long.")] string Name)
    : IRequest<SpecializationDto>;
