using MediatR;

namespace Vitrina.UseCases.UserSpecialization.DeleteSpecialization;

/// <summary>
///     Specialization removal command
/// </summary>
public record DeleteSpecializationCommand(int Id) : IRequest<SpecializationDto>;
