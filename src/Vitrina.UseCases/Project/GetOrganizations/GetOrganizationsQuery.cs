using MediatR;

namespace Vitrina.UseCases.Project.GetOrganizations;

/// <summary>
/// Get organizations.
/// </summary>
public class GetOrganizationsQuery : IRequest<ICollection<string>>
{
}
