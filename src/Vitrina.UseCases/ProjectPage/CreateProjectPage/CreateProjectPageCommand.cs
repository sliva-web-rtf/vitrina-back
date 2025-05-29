using MediatR;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.CreateProjectPage;

/// <inheritdoc />
public record CreateProjectPageCommand(CreateProjectPageDto PageDto, int? IdAuthorizedUser) : IRequest<Guid>;
