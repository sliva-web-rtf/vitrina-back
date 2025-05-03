namespace Vitrina.Domain.User;

public abstract class NotStudentBase : UserWithRoleBase
{
    /// <summary>
    ///     Place of work
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    ///     Position in the company.
    /// </summary>
    public string? Post { get; set; }
}
