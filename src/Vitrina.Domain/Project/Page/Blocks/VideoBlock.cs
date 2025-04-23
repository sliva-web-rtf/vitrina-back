using Vitrina.Domain.File;

namespace Vitrina.Domain.Project.Page.Blocks;

public class VideoBlock : NumberedBlockBase
{
    /// <summary>
    ///     Link to the video.
    /// </summary>
    required public File.File Video { get; set; }
}
