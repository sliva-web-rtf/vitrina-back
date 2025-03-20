using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.User.Specialization;

/// <summary>
/// Query for all specializations
/// </summary>
public record GetAllSpecializationsQuery : IRequest<SpecializationDto[]>;
