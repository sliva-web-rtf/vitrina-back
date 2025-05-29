using AutoMapper;
using Vitrina.Domain.Project.Teammate;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.ProjectTeam.Role;

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
        CreateMap<ProjectRole, ResponceRoleDto>().ReverseMap();
        CreateMap<Domain.Project.Project, CreateProjectDto>().ReverseMap();
        CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
    }
}
