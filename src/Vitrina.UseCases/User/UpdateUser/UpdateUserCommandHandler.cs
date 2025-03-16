using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.Domain.User;

/// <inheritdoc />
public class UpdateUserCommandHandler<TUpdateDto, TResultDto>(
    IMapper mapper,
    IUserRepository userRepository)
    : IRequestHandler<UpdateUserCommand<TUpdateDto, TResultDto>, TResultDto> where TUpdateDto : class
{
    /// <inheritdoc />
    public async Task<TResultDto> Handle(UpdateUserCommand<TUpdateDto, TResultDto> request, CancellationToken cancellationToken)
    {
        if (request.PatchDocument is null)
        {
            throw new DomainException("Invalid JSON");
        }

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var dto = mapper.Map<TUpdateDto>(user);
        request.PatchDocument.ApplyTo(dto);

        // костыль(
        if (dto is UpdateStudentDto updateStudentDto)
        {
            if (!CheckEducationCourse(updateStudentDto.EducationCourse, updateStudentDto.EducationLevel))
            {
                throw new DomainException("The education course does not correspond to the education level.");
            }
        }
        // )
        mapper.Map(dto, user);
        await userRepository.UpdateAsync(user, cancellationToken);
        return mapper.Map<TResultDto>(user);
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
