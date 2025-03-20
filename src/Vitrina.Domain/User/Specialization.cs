using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.User;

public class Specialization
{
    /// <summary>
    /// ID of the specialization.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name of the specialization.
    /// </summary>
    public string Name { get; init; }
}
