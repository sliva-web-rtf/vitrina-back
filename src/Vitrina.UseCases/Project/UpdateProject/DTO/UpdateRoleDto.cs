namespace Vitrina.UseCases.Project.UpdateProject.DTO;

/// <summary>
/// Dto for update.
/// </summary>
public class UpdateRoleDto
{
    /// <summary>
    /// Role id.
    /// </summary>ы
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; init; }
}
