using MediatR;

namespace Vitrina.UseCases.Project.GetTypes;

/// <summary>
///     Get project types query.
/// </summary>
public class GetTypesQuery : IRequest<ICollection<string>>
{
}
