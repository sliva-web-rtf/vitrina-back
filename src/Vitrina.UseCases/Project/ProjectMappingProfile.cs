using AutoMapper;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.SearchProjects;
using Vitrina.UseCases.Project.SearchProjects.V2;
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
        CreateMap<Domain.Project.Project, AddProjectCommand>().ReverseMap();
        CreateMap<Domain.Project.Content, ContentDto>().ReverseMap();
        CreateMap<Domain.Project.Tag, TagDto>().ReverseMap();
        CreateMap<Domain.Project.Role, RoleDto>().ReverseMap();
        CreateMap<Domain.Project.User, UserDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ProjectDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectDto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectV2Dto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();

        CreateMap<UpdateUserDto, Domain.Project.User>()
            .ForMember(u => u.ProjectId, dest => dest.Ignore())
            .ForMember(u => u.Roles, dest => dest.Ignore())
            .ForMember(u => u.Project, dest => dest.Ignore());
        CreateMap<Domain.Project.User, UpdateUserDto>()
            .ForMember(u => u.Roles, dest => dest.Ignore());
        CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
        CreateMap<Domain.Project.Role, UpdateRoleDto>().ReverseMap();
        CreateMap<Domain.Project.Block, BlockDto>().ReverseMap();
    }
}
