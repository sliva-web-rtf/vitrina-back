using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain;

public class BaseEntity<TKey>
{
    /// <summary>
    ///     ID.
    /// </summary>
    [Key]
    required public TKey Id { get; init; }
}
