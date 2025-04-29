namespace Vitrina.UseCases.Auth;

/// <summary>
///     Helper to generate confirmation code.
/// </summary>
internal static class ConfirmationCodeGenerator
{
    public static int Generate()
    {
        var random = new Random();
        return random.Next(100000, 1000000);
    }
}
