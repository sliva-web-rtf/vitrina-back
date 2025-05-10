namespace Vitrina.Domain.Project;

public class Block
{
    /// <summary>
    /// ID of block.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of block.
    /// </summary>
    required public string Title { get; set; }

    /// <summary>
    /// Text of block.
    /// </summary>
    required public string Text { get; set; }

    /// <summary>
    ///     Sequence number.
    /// </summary>
    public int SequenceNumber { get; set; }

    /// <summary>
    /// Id og project.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// Project.
    /// </summary>
    public Project? Project { get; set; }
}
