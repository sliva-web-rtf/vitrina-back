using AutoMapper;
using Vitrina.Domain.Project.Teammate;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project.UpdateProject.DTO;

namespace Vitrina.UseCases.Project;

/// <summary>
/// Project mapping.
/// </summary>
public class ProjectMappingProfile : Profile
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProjectMappingProfile()
    {
        CreateMap<ProjectRole, RoleDto>().ReverseMap();
        CreateMap<Teammate, UserDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ProjectDto>().ReverseMap();
        CreateMap<UpdateUserDto, Teammate>()
            .ForMember(teammate => teammate.ProjectId, member => member.Ignore())
            .ForMember(teammate => teammate.Roles, member => member.Ignore())
            .ForMember(teammate => teammate.Project, member => member.Ignore());
        CreateMap<Teammate, UpdateUserDto>()
            .ForMember(updateUserDto => updateUserDto.Roles, member => member.Ignore());
        CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
        CreateMap<ProjectRole, UpdateRoleDto>().ReverseMap();
    }
}
