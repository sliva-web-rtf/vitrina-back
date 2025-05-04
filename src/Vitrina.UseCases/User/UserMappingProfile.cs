using AutoMapper;
using Vitrina.Domain.User;
using Vitrina.UseCases.Auth.GetUserById;
using Vitrina.UseCases.Specialization;
using Vitrina.UseCases.User.DTO.Profile;
using Vitrina.UseCases.User.DTO.Profile.Base;
using Student = Vitrina.Domain.User.Student;
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
        CreateMap<Domain.User.User, UserDto>()
            .ReverseMap();
        CreateMap<Domain.User.User, UserDetailsDto>();
        CreateMap<Student, StudentDto>()
            .ReverseMap();
        CreateMap<Curator, NotStudentDto>();
        CreateMap<Partner, NotStudentDto>();
        CreateMap<Domain.User.Specialization, SpecializationDto>();
    }
}
