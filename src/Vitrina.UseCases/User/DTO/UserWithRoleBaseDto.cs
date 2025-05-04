namespace Vitrina.UseCases.User.DTO.Profile.Base;

public abstract record UserWithRoleBaseDto
{
    public UserDto User { get; set; }
}
