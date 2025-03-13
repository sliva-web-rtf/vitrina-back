using Vitrina.Domain.User;

namespace Vitrina.UseCases.Common;

public record PartnerDto(
    RoleOnPlatformEnum RoleOnPlatform,
    string FirstName,
    string LastName,
    string Patronymic,
    string Telegram,
    string Email,
    string PhoneNumber,
    string Company,
    string Post);
