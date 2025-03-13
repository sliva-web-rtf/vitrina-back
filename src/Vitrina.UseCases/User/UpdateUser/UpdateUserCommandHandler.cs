using AutoMapper;
using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.Domain.User;

/// <inheritdoc />
public class UpdateUserCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, UpdateUserCommandResult>
{
    /// <inheritdoc />
    public async Task<UpdateUserCommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = mapper.Map<UpdateUserDto>(request.User);
        request.patchDocument.ApplyTo(dto);

        if (request.User.RoleOnPlatform == RoleOnPlatformEnum.Student)
        {
            if (!CheckEducationCourse(dto.EducationCourse, dto.EducationLevel))
            {
                return new UpdateUserCommandResult(
                    false,
                    "The education course does not correspond to the education level.");
            }
        }

        mapper.Map<UpdateUserDto>(request.User);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return new UpdateUserCommandResult();
    }

    private bool CheckEducationCourse(int educationCourse, EducationLevelEnum educationLevel)
    {
        return educationLevel switch
        {
            EducationLevelEnum.Bachelors => educationCourse is > 0 and < 5,
            EducationLevelEnum.Specialty => educationCourse is > 0 and < 6,
            EducationLevelEnum.Magistracy => educationCourse is > 0 and < 3,
            EducationLevelEnum.Postgraduate => educationCourse is > 0 and < 5,
            EducationLevelEnum.NotStudent => true,
            _ => false
        };
    }
}
