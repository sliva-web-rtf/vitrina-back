using MediatR;

namespace Vitrina.UseCases.UserSpecialization.GetAllSpecializations;

/// <summary>
///     Query for all specializations
/// </summary>
public record GetAllSpecializationsQuery : IRequest<SpecializationDto[]>;
