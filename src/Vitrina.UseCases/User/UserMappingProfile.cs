using AutoMapper;
using Newtonsoft.Json;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Auth.Register;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.Specialization;
using Vitrina.UseCases.User.DTO;
using Vitrina.UseCases.User.DTO.AdditionalInformation;

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
        CreateMap<RegisterCommand, UpdateUserDtoBase>().IgnoreAllNonExisting();
        CreateMap<RegisterCommand, Domain.User.User>()
            .ForMember(user => user.UserName, member =>
                member.MapFrom(registerCommand => registerCommand.FirstName))
            .ForAllMembers(member => member.Ignore());
        CreateMap<Domain.User.User, UserDetailsDto>();
        CreateMap<StudentDtoBase, Domain.User.User>()
            .ForMember(user => user.AdditionalInformation, member => member.MapFrom(userDto =>
                JsonConvert.SerializeObject(userDto.AdditionalInformation)))
            .IgnoreAllNonExisting();
        CreateMap<Domain.User.User, StudentDtoBase>()
            .ForMember(userDto => userDto.AdditionalInformation, member =>
                member.MapFrom(user =>
                    JsonConvert.DeserializeObject<AdditionalStudentInfo>(user.AdditionalInformation)));
        CreateMap<NotStudentDtoBase, Domain.User.User>()
            .ForMember(user => user.AdditionalInformation, member =>
                member.MapFrom(userDto => JsonConvert.SerializeObject(userDto.AdditionalInformation)))
            .IgnoreAllNonExisting();
        CreateMap<Domain.User.User, NotStudentDtoBase>()
            .ForMember(userDto => userDto.AdditionalInformation, member => member.MapFrom(user =>
                JsonConvert.DeserializeObject<AdditionalNotStudentInfo>(user.AdditionalInformation)));
        CreateMap<Domain.User.Specialization, SpecializationDto>();
        CreateMap<Domain.User.User, UpdateUserDtoBase>()
            .ForMember(userDto => userDto.AdditionalInformation, member => member.MapFrom(user =>
                JsonConvert.DeserializeObject<AdditionalUserInfo>(user.AdditionalInformation)));
        CreateMap<UpdateUserDtoBase, StudentDtoBase>();
        CreateMap<UpdateUserDtoBase, NotStudentDtoBase>();
        CreateMap<AdditionalUserInfo, AdditionalStudentInfo>();
        CreateMap<AdditionalUserInfo, AdditionalNotStudentInfo>();
        CreateMap<Domain.User.User, UpdateUserDto>()
            .ForMember(user => user.Roles, member => member.Ignore());
    }
}
