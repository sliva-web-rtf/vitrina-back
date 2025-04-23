namespace Vitrina.Domain.Project.Page.BasicContentUnits;

/// <summary>
/// Content unit consisting of an image and text.
/// </summary>
public class UnitWithImageAndText
{
    /// <summary>
    ///     A row with styles for an image.
    /// </summary>
    public File.File? Css { get; set; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public File.File Image { get; set; }

    /// <summary>
    ///     Link to an html document in the cloud.
    /// </summary>
    required public File.File Html { get; set; }
}
