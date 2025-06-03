using MediatR;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project command.
/// </summary>
public record CreateProjectCommand(CreateProjectDto ProjectDto, int IdAuthorizedUser) : IRequest<int>;
