using Vitrina.Domain.User;

namespace Vitrina.UseCases.Common;

public abstract record NotStudentDtoBase
{
    public RoleOnPlatformEnum RoleOnPlatform { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Patronymic { get; init; }

    public string Telegram { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string Company { get; init; }

    public string Post { get; init; }
}
