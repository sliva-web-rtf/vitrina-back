namespace Vitrina.UseCases.Common.DTO;

public class FileDto
{
    /// <summary>
    ///     The link to the cloud storage.
    /// </summary>
    required public string Link { get; init; }

    /// <summary>
    ///     File Extension.
    /// </summary>
    required public string Extension { get; init; }
}
