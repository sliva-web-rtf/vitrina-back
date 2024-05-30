using AutoMapper;
using Saritasa.RedMan.Domain.Users;
using Saritasa.RedMan.UseCases.Users.Common.Dtos;
using Saritasa.RedMan.UseCases.Users.GetUserById;

namespace Saritasa.RedMan.UseCases.Users;

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
    }
}
