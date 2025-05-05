using AutoMapper;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.SearchProjects;
using Vitrina.UseCases.Project.SearchProjects.V2;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.User.DTO;
using UpdateUserDto = Vitrina.UseCases.Project.UpdateProject.DTO.UpdateUserDto;

namespace Vitrina.UseCases.Project;

/// <summary>
///     Project mapping.
/// </summary>
public class ProjectMappingProfile : Profile
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public ProjectMappingProfile()
    {
        CreateMap<Domain.Project.Project, AddProjectCommand>().ReverseMap();
        CreateMap<Content, ContentDto>().ReverseMap();
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<ProjectRole, RoleDto>().ReverseMap();
        CreateMap<Teammate, UserDto>()
            .IncludeMembers(teammate => teammate.User);
        CreateMap<Domain.Project.Project, ProjectDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectDto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectV2Dto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();
        CreateMap<Teammate, UpdateUserDto>()
            .IncludeMembers(teammate => teammate.User)
            .ForMember(
                updateUserDto => updateUserDto.Roles,
                memberConfiguration => memberConfiguration.MapFrom(teammate => teammate.Roles));
        CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
        CreateMap<ProjectRole, UpdateRoleDto>().ReverseMap();
        CreateMap<Block, BlockDto>().ReverseMap();
        CreateMap<Domain.Project.Project, PreviewProjectDto>();
    }
}
