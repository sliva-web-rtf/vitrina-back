using AutoMapper;
using Vitrina.Domain.User;
using Vitrina.UseCases.Auth.GetUserById;
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
        CreateMap<User, UserDto>();
        CreateMap<User, UserDetailsDto>();
        CreateMap<User, RegisterCommand>()
            .ForMember(u => u.PasswordConfirm, dest => dest.Ignore())
            .ForMember(u => u.Password, dest => dest.Ignore());
    }
}
