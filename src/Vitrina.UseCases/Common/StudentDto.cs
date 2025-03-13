using Vitrina.Domain.User;

namespace Vitrina.UseCases.Common;

public record StudentDto(
    EducationLevelEnum EducationLevel,
    int EducationCourse,
    RoleOnPlatformEnum RoleOnPlatform,
    string FirstName,
    string LastName,
    string Patronymic,
    string Telegram,
    string Email,
    string PhoneNumber,
    string Resume,
    ICollection<string> RolesInTeam,
    ICollection<Domain.Project.Project> Projects,
    ICollection<Specialization> Specializations);
