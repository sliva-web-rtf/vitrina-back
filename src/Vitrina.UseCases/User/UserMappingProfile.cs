using AutoMapper;
using Vitrina.Domain.User;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Auth.Register;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User;

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
                    .ToList()))
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<UpdateStudentDto, Domain.User.User>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<Domain.User.User, PartnerDto>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<PartnerDto, Domain.User.User>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<Domain.User.User, CuratorDto>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<CuratorDto, Domain.User.User>()
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<Domain.User.Specialization, SpecializationDto>();
    }
}
