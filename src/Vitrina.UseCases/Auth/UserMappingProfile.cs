using AutoMapper;
using Vitrina.Domain.User;
using Vitrina.UseCases.Auth.Register;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Auth;

/// <summary>
/// User mapping profile.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(userDto => userDto.Avatar, member => member.Ignore())
            .ForMember(userDto => userDto.Roles, member => member.Ignore());
        CreateMap<User, RegisterCommand>()
            .ForMember(registerCommand => registerCommand.PasswordConfirm, member => member.Ignore())
            .ForMember(registerCommand => registerCommand.Password, member => member.Ignore());
    }
}
