using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project;

/// <summary>
/// Role of user in team.
/// </summary>
public class Role
{
    /// <summary>
    /// Id.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Role users.
    /// </summary>
    public ICollection<User> Users { get; private set; } = new List<User>();
}
