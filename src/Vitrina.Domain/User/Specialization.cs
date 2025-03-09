using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.User;

public class Specialization
{
    [Key]
    public string Name { get; init; }
}
