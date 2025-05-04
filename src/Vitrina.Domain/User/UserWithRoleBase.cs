namespace Vitrina.Domain.User;

public abstract class UserWithRoleBase
{
    public User User { get; set; }

    public int UserId { get; set; }
}
