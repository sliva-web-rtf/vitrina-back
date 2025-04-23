using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain;

public class BaseEntity<TId>
{
    /// <summary>
    ///     ID.
    /// </summary>
    [Key]
    required public TId Id { get; init; }
}
