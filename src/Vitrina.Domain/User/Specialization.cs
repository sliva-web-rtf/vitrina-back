using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.User;

public class Specialization
{
    /// <summary>
    ///     ID of the specialization.
    /// </summary>
    [Key]
    required public Guid Id { get; init; }

    /// <summary>
    ///     Name of the specialization.
    /// </summary>
    required public string Name { get; init; }
}
