using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common.DTO;

public class FileDto
{
    /// <summary>
    ///     The link to the cloud storage.
    /// </summary>
    [Url(ErrorMessage = "Incorrect URL")]
    required public string Link { get; init; }

    /// <summary>
    ///     File Extension.
    /// </summary>
    [RegularExpression(@"^\.[a-zA-Z0-9]{1,20}$", ErrorMessage = "Incorrect file extension")]
    required public string Extension { get; init; }
}
