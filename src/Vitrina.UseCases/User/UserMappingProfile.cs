using AutoMapper;
using Vitrina.Domain.User;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Auth.Register;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.User.DTO.Profile;

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
        CreateMap<Domain.User.User, UserDto>();
        CreateMap<Domain.User.User, UserDetailsDto>();
        CreateMap<Domain.User.User, RegisterCommand>()
            .ForMember(u => u.PasswordConfirm, dest => dest.Ignore())
            .ForMember(u => u.Password, dest => dest.Ignore());
        CreateMap<Domain.User.User, StudentDto>()
            .ForMember(
                dest => dest.Projects,
                opt => opt.MapFrom(src => src.PositionsInTeams
                    .Select(teammate => teammate.Project)
                    .Distinct()
                    .ToList()));
        CreateMap<UpdateStudentDto, Domain.User.User>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<Domain.User.User, PartnerDto>().ReverseMap();
        CreateMap<Domain.User.User, CuratorDto>().ReverseMap();
        CreateMap<Specialization, SpecializationDto>();
    }
}
