namespace Vitrina.UseCases.User.Auth.ChangePassword;

public class ChangePasswordCommandResult
{
    public bool IsSuccess { get; init; }

    public string Token { get; init; }
}
