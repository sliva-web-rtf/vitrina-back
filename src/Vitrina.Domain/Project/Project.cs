using System.ComponentModel.DataAnnotations;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;

namespace Vitrina.Domain.Project;

/// <summary>
///     Domain class of project.
/// </summary>
public class Project
{
    [Key] public int Id { get; init; }

    /// <summary>
    ///     Project name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    ///     Project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    ///     Project client.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    ///     Project page id.
    /// </summary>
    required public Guid PageId { get; init; }

    /// <summary>
    ///     Priority of project.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///     Project sphere.
    /// </summary>
    public virtual ProjectSphere? Sphere { get; set; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public virtual ProjectThematics? Thematics { get; set; }

    /// <summary>
    ///     Project page content.
    /// </summary>
    public virtual ProjectPage Page { get; init; }

    /// <summary>
    ///     Creator's ID.
    /// </summary>
    required public int CreatorId { get; init; }

    /// <summary>
    ///     Project team id.
    /// </summary>
    public Guid TeamId { get; init; }

    /// <summary>
    ///     Project team.
    /// </summary>
    public virtual Team? Team { get; set; }

    /// <summary>
    ///     The project supervisor's ID.
    /// </summary>
    public int? CuratorId { get; set; }

    /// <summary>
    ///     Project curator.
    /// </summary>
    public virtual User.User? Curator { get; set; }

    /// <summary>
    ///     Checks the user's editing rights.
    ///     If the user with the passed id is not allowed to make changes to the project, an exception is generated.
    /// </summary>
    public void ThrowExceptionIfNoAccessRights(int idAuthorizedUser)
    {
        if (!(CreatorId == idAuthorizedUser || CuratorId == idAuthorizedUser ||
              (Team is not null && Team.TeamMembers.Any(teammate => teammate.UserId == idAuthorizedUser))))
        {
            throw new ForbiddenException("You do not have the rights to change the data of this project.");
        }
    }
}
