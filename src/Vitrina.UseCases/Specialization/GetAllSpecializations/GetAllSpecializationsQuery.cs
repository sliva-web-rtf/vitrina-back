using MediatR;

namespace Vitrina.UseCases.Specialization.GetAllSpecializations;

/// <summary>
/// Query for all specializations
/// </summary>
public record GetAllSpecializationsQuery : IRequest<SpecializationDto[]>;
