using System.Text.Json;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.Profile;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vitrina.UseCases.User.UpdateUser;

/// <inheritdoc />
public class UpdateUserProfileCommandHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<UpdateUserProfileCommand, JsonDocument>
{
    /// <inheritdoc />
    public async Task<JsonDocument> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (request.User is null)
        {
            throw new DomainException("Invalid JSON");
        }

        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        if (request.User.RoleOnPlatform is null || request.User.RoleOnPlatform != user.RoleOnPlatform)
        {
            throw new DomainException("The user's role cannot be changed.");
        }

        await UpdateUser(user, request.User, cancellationToken);
        return JsonDocument.Parse(user.ProfileData);
    }

    private async Task UpdateUser(Domain.User.User user, UpdateUserDto userDto, CancellationToken cancellationToken)
    {
        switch (user.RoleOnPlatform)
        {
            case RoleOnPlatformEnum.Student:
                var studentDto = GetDtoRole<StudentDto>(user, userDto);
                PerformEducationValidation(studentDto.EducationCourse, studentDto.EducationLevel);
                await ApplyChanges(studentDto, user, cancellationToken);
                break;
            case RoleOnPlatformEnum.Curator:
                var curatorDto = GetDtoRole<CuratorDto>(user, userDto);
                await ApplyChanges(curatorDto, user, cancellationToken);
                break;
            case RoleOnPlatformEnum.Partner:
                var partnerDto = GetDtoRole<PartnerDto>(user, userDto);
                await ApplyChanges(partnerDto, user, cancellationToken);
                break;
            default: throw new DomainException("The method is not implemented on the platform for this role.");
        }
    }

    private TDtoRoles GetDtoRole<TDtoRoles>(Domain.User.User user, UpdateUserDto userDto)
    {
        var dtoRoles = JsonConvert.DeserializeObject<TDtoRoles>(user.ProfileData);
        mapper.Map(userDto, dtoRoles);
        return dtoRoles;
    }

    private async Task ApplyChanges<TDto>(TDto dto, Domain.User.User user, CancellationToken cancellationToken)
    {
        mapper.Map(dto, user);
        user.ProfileData = JsonSerializer.Serialize(dto);
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    private void PerformEducationValidation(int educationCourse, EducationLevelEnum educationLevel)
    {
        var status = educationLevel switch
        {
            EducationLevelEnum.Bachelors => educationCourse is > 0 and < 5,
            EducationLevelEnum.Specialty => educationCourse is > 0 and < 6,
            EducationLevelEnum.Magistracy => educationCourse is > 0 and < 3,
            EducationLevelEnum.Postgraduate => educationCourse is > 0 and < 5,
            EducationLevelEnum.NotStudent => true,
            _ => false
        };

        if (!status)
        {
            throw new DomainException("The education course does not correspond to the education level.");
        }
    }
}
