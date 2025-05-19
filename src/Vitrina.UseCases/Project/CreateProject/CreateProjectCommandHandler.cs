using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.CreateProject;

/// <summary>
///     Add project handler.
/// </summary>
internal class CreateProjectCommandHandler(IMapper mapper, IAppDbContext dbContext)
    : IRequestHandler<CreateProjectCommand, int>
{
    /// <inheritdoc />
    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = mapper.Map<ProjectDto, Domain.Project.Project>(request.ProjectDto);
        // TODO: прописать валидацию страницы при создания проекта
        // 1) проверить, что проекта с таким PageID не существует
        // 2) проверить, что существует страница с указанным PageID
        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
