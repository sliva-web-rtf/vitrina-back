using MediatR;

namespace Vitrina.UseCases.Project.GetProjectsIds;

/// <summary>
///     Get projects ids query.
/// </summary>
public class GetProjectIdsQuery : IRequest<ICollection<int>>;
