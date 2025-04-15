using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain;

public class BaseEntity<TId>
{
    /// <summary>
    ///     ID.
    /// </summary>
    [Key]
    public TId Id { get; init; }
}
