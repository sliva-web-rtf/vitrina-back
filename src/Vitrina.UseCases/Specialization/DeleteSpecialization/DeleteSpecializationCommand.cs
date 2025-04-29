using MediatR;

namespace Vitrina.UseCases.Specialization.DeleteSpecialization;

/// <summary>
///     Specialization removal command
/// </summary>
public record DeleteSpecializationCommand(int Id) : IRequest<SpecializationDto>;
