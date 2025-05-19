using FluentValidation;
using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDtoBase>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(user => user.Telegram)
            .Matches("^@[a-zA-Z][a-zA-Z0-9_-]{4,31}$").WithMessage("This is an incorrect Telegram username.");
        RuleFor(user => user.Email)
            .MaximumLength(255).WithMessage("Email: allowed length is exceeded")
            .EmailAddress().WithMessage("Incorrect email");
        RuleFor(user => user.PhoneNumber)
            .Matches(@"^\+7\d{10}$").WithMessage("Incorrect phone number.");
        RuleFor(user => user.Patronymic)
            .MaximumLength(255).WithMessage("Patronymic: allowed length is exceeded")
            .Matches(@"^(?!.*\d).+$").WithMessage("The patronymic contains invalid characters.");
        RuleFor(user => user.LastName)
            .MaximumLength(255).WithMessage("Last name: allowed length is exceeded")
            .Matches(@"^(?!.*\d).+$").WithMessage("The last name contains invalid characters.");
        RuleFor(user => user.FirstName)
            .MaximumLength(255).WithMessage("First name: allowed length is exceeded")
            .Matches(@"^(?!.*\d).+$").WithMessage("The first name contains invalid characters.");
        RuleFor(user => user.AdditionalInformation)
            .Must(info => PerformEducationValidation(info.EducationCourse, info.EducationLevel))
            .WithMessage("The education course does not correspond to the education level.");
        RuleFor(user => user.AdditionalInformation.RoleInTeam)
            .MaximumLength(255).WithMessage("RoleInTeam: allowed length is exceeded");
        RuleFor(user => user.AdditionalInformation.Resume)
            .MaximumLength(255).WithMessage("Resume: allowed length is exceeded");
        RuleFor(user => user.AdditionalInformation.Company)
            .MaximumLength(255).WithMessage("Company: allowed length is exceeded");
        RuleFor(user => user.AdditionalInformation.Post)
            .MaximumLength(255).WithMessage("Post: allowed length is exceeded");
    }

    private bool PerformEducationValidation(int educationCourse, EducationLevelEnum educationLevel) =>
        educationLevel switch
        {
            EducationLevelEnum.Bachelors => educationCourse is > 0 and < 5,
            EducationLevelEnum.Specialty => educationCourse is > 0 and < 6,
            EducationLevelEnum.Magistracy => educationCourse is > 0 and < 3,
            EducationLevelEnum.Postgraduate => educationCourse is > 0 and < 5,
            EducationLevelEnum.NotStudent => true,
            _ => false
        };
}
