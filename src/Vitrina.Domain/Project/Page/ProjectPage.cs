using Saritasa.Tools.Domain.Exceptions;

namespace Vitrina.Domain.Project.Page;

/// <inheritdoc />
public class ProjectPage : BaseEntity<Guid>
{
    /// <summary>
    ///     Users who can edit the page.
    /// </summary>
    public virtual ICollection<PageEditor> Editors { get; init; } = new List<PageEditor>();

    /// <summary>
    ///     Page status.
    /// </summary>
    required public PageReadyStatusEnum ReadyStatus { get; set; }

    /// <summary>
    ///     Page content blocks.
    /// </summary>
    public virtual ICollection<ContentBlock> ContentBlocks { get; private set; } = new List<ContentBlock>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public Guid? ProjectId { get; init; }

    /// <summary>
    ///     Project.
    /// </summary>
    public Project? Project { get; init; }

    public void NumberCustomBlocks() =>
        ContentBlocks = ContentBlocks
            .Select((block, index) =>
            {
                block.NumberOnPage = index;
                return block;
            })
            .ToList();

    public void SortContentBlocks() =>
        ContentBlocks = ContentBlocks
            .OrderBy(contentBlock => contentBlock.NumberOnPage)
            .ToList();

    /// <summary>
    ///     Checks the user's editing rights.
    ///     If the user with the passed id is not allowed to make changes to the project page, an exception is generated.
    /// </summary>
    public void ThrowExceptionIfNoAccessRights(int? idAuthorizedUser)
    {
        if (idAuthorizedUser is null || Editors.All(editor => editor.UserId != idAuthorizedUser))
        {
            throw new ForbiddenException("You have no access to work with this page.");
        }
    }
}
