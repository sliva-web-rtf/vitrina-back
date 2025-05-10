using AutoMapper;
using Newtonsoft.Json;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Auth.Register;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Specialization;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;
using UpdateUserDto = Vitrina.UseCases.User.DTO.UpdateUserDto;
using UserDto = Vitrina.UseCases.Common.DTO.UserDto;

namespace Vitrina.UseCases.User;

/// <summary>
///     User mapping profile.
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public UserMappingProfile()
    {
        CreateMap<RegisterCommand, UpdateUserDto>().IgnoreAllNonExisting();
        CreateMap<RegisterCommand, Domain.User.User>()
            .ForMember(user => user.UserName, member =>
                member.MapFrom(registerCommand => registerCommand.FirstName))
            .ForAllMembers(member => member.Ignore());
        CreateMap<Domain.User.User, UserDto>()
            .ReverseMap();
        CreateMap<Domain.User.User, UserDetailsDto>();
        CreateMap<StudentDto, Domain.User.User>()
            .ForMember(user => user.AdditionalInformation, member => member.MapFrom(userDto =>
                JsonConvert.SerializeObject(userDto.AdditionalInformation)))
            .IgnoreAllNonExisting();
        CreateMap<Domain.User.User, StudentDto>()
            .ForMember(userDto => userDto.AdditionalInformation, member =>
                member.MapFrom(user =>
                    JsonConvert.DeserializeObject<AdditionalStudentInfo>(user.AdditionalInformation)));
        CreateMap<NotStudentDto, Domain.User.User>()
            .ForMember(user => user.AdditionalInformation, member =>
                member.MapFrom(userDto => JsonConvert.SerializeObject(userDto.AdditionalInformation)))
            .IgnoreAllNonExisting();
        CreateMap<Domain.User.User, NotStudentDto>()
            .ForMember(userDto => userDto.AdditionalInformation, member => member.MapFrom(user =>
                JsonConvert.DeserializeObject<AdditionalNotStudentInfo>(user.AdditionalInformation)));
        CreateMap<Domain.User.Specialization, SpecializationDto>();
        CreateMap<Domain.User.User, UpdateUserDto>()
            .ForMember(userDto => userDto.AdditionalInformation, member => member.MapFrom(user =>
                JsonConvert.DeserializeObject<AdditionalUserInfo>(user.AdditionalInformation)));
        CreateMap<UpdateUserDto, StudentDto>();
        CreateMap<UpdateUserDto, NotStudentDto>();
        CreateMap<AdditionalUserInfo, AdditionalStudentInfo>();
        CreateMap<AdditionalUserInfo, AdditionalNotStudentInfo>();
        CreateMap<Domain.User.User, Project.UpdateProject.DTO.UpdateUserDto>()
            .ForMember(user => user.Roles, member => member.Ignore());
    }
}
