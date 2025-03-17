using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User;

/// <summary>
/// Functionality for working with users.
/// </summary>
public class HandlerUserActions(IUserRepository userRepository, IMapper mapper) : IHandlerUserActions
{
    /// <summary>
    /// Getting a user by ID.
    /// </summary>
    public async Task<TResultDto> GetUserById<TResultDto>(int userId, CancellationToken cancellationToken)
    {
        return mapper.Map<TResultDto>(await userRepository.GetByIdAsync(userId, cancellationToken) ??
                                      throw new NotFoundException("The user with the specified Id was not found"));
    }

    /// <summary>
    /// User update.
    /// </summary>
    public async Task<TResultDto> UpdateById<TUpdateDto, TResultDto>(
        int userId,
        JsonPatchDocument<TUpdateDto> patchDocument,
        CancellationToken cancellationToken) where TUpdateDto : class
    {
        if (patchDocument is null)
        {
            throw new DomainException("Invalid JSON");
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var dto = mapper.Map<TUpdateDto>(user);
        patchDocument.ApplyTo(dto);

        if (dto is UpdateStudentDto updateStudentDto)
        {
            if (!CheckEducationCourse(updateStudentDto.EducationCourse, updateStudentDto.EducationLevel))
            {
                throw new DomainException("The education course does not correspond to the education level.");
            }
        }

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
