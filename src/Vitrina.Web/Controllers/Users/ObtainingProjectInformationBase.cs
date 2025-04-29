using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vitrina.UseCases.ProjectPages;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.GetUserProjects;
using Vitrina.UseCases.User.GetUserProjectsPages;

namespace Vitrina.Web.Controllers.Users;

public abstract class ObtainingProjectInformationBase(IMediator mediator)
{
    /// <summary>
    ///     Retrieves the list of project pages of the user with the specified id.
    /// </summary>
    public virtual async Task<ICollection<ProjectPageDto>> GetProjectPages([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProjectPagesByUserIdQuey(id);
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Retrieves the list of projects of the user with the specified id.
    /// </summary>
    public virtual async Task<ICollection<PreviewProjectDto>> GetProjects([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetUserProjectsByUserIdQuery(id);
        return await mediator.Send(query, cancellationToken);
    }
}
