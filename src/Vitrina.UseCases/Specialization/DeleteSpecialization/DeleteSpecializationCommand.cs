using MediatR;

namespace Vitrina.UseCases.Common.DTO.DeleteSpecialization;

/// <summary>
/// Specialization removal command
/// </summary>
public record DeleteSpecializationCommand(int Id) : IRequest<SpecializationDto>;

