namespace Vitrina.Domain.Project.Page.BasicContentUnits;

public abstract class UnitWithImageBase : BaseEntity<Guid>
{
    /// <summary>
    /// File ID.
    /// </summary>
    public Guid ImageFileId { get; set; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public File.File Image { get; set; }
}
