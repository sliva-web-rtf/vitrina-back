﻿using AutoMapper;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.SearchProjects;
using Vitrina.UseCases.Project.SearchProjects.V2;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using UpdateUserDto = Vitrina.UseCases.Project.UpdateProject.DTO.UpdateUserDto;

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
        CreateMap<Content, ContentDto>().ReverseMap();
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<ProjectRole, RoleDto>().ReverseMap();
        CreateMap<Teammate, UserDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ProjectDto>().ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectDto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();
        CreateMap<Domain.Project.Project, ShortProjectV2Dto>()
            .ForMember(p => p.ImageUrl, dest => dest.Ignore())
            .ReverseMap();

        CreateMap<UpdateUserDto, Domain.Project.Teammate>()
            .ForMember(u => u.ProjectId, dest => dest.Ignore())
            .ForMember(u => u.Roles, dest => dest.Ignore())
            .ForMember(u => u.Project, dest => dest.Ignore());
        CreateMap<Domain.Project.Teammate, UpdateUserDto>()
            .ForMember(u => u.Roles, dest => dest.Ignore());
        CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
        CreateMap<ProjectRole, UpdateRoleDto>().ReverseMap();
        CreateMap<Block, BlockDto>().ReverseMap();
        CreateMap<Domain.Project.Project, PreviewProjectDto>();
    }
}
