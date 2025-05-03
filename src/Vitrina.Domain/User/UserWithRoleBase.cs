namespace Vitrina.Domain.User;

public abstract class UserWithRoleBase : BaseEntity<Guid>
{
    public User User { get; set; }

    public int UserId { get; set; }
}
