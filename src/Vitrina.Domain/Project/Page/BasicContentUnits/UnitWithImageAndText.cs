namespace Vitrina.Domain.Project.Page.BasicContentUnits;

/// <summary>
/// Content unit consisting of an image and text.
/// </summary>
public class UnitWithImageAndText : UnitWithImageBase
{
    /// <summary>
    ///     HTML block marking..
    /// </summary>
    required public string Html { get; set; }
}
