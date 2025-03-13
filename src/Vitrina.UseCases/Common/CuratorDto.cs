namespace Vitrina.Domain.User;

public record CuratorDto(
    RoleOnPlatformEnum RoleOnPlatform,
    string FirstName,
    string LastName,
    string Patronymic,
    string Telegram,
    string Email,
    string PhoneNumber,
    string Company,
    string Post);
