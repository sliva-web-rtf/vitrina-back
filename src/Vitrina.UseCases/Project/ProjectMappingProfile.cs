using AutoMapper;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;

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
    }
}
