using MediatR;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
///     Get project query.
/// </summary>
public record GetProjectByIdQuery(Guid Id) : IRequest<ResponceProjectDto>;
