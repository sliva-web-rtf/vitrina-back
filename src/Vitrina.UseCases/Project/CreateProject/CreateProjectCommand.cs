using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project command.
/// </summary>
public record CreateProjectCommand(ProjectDto ProjectDto) : IRequest<int>;
