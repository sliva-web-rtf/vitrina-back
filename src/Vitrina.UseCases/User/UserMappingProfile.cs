using AutoMapper;
using Newtonsoft.Json;
using Vitrina.Domain.User;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.User.Auth.Register;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;
using Vitrina.UseCases.UserSpecialization;

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
        CreateMap<RegisterCommand, UserDto>().IgnoreAllNonExisting();
        CreateMap<RegisterCommand, Domain.User.User>()
            .ForMember(user => user.UserName, member =>
                member.MapFrom(registerCommand => registerCommand.FirstName))
            .ForAllMembers(member => member.Ignore());
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
        CreateMap<Specialization, SpecializationDto>();
        CreateMap<Domain.User.User, UserDto>()
            .ForMember(userDto => userDto.AdditionalInformation, member => member.MapFrom(user =>
                JsonConvert.DeserializeObject<AdditionalUserInfo>(user.AdditionalInformation)));
        CreateMap<UserDto, StudentDto>();
        CreateMap<UserDto, NotStudentDto>();
        CreateMap<AdditionalUserInfo, AdditionalStudentInfo>();
        CreateMap<AdditionalUserInfo, AdditionalNotStudentInfo>();
        CreateMap<Domain.User.User, ResponceShortenedUserDto>().ReverseMap();
    }
}
