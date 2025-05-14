using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
///     Add project command.
/// </summary>
public record CreateProjectCommand : ProjectDto, IRequest<int>;
