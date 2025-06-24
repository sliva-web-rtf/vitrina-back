using AutoMapper;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Project.UpdateProject.DTO;

namespace Vitrina.UseCases.Tests.Infrastructure;

internal static class TestMapper
{
    internal static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<TestMappingProfile>();
            });

        return configuration.CreateMapper();
    }

    /// <summary>
    /// Профиль автомаппера для тестов.
    /// </summary>
    private class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<AddProjectCommand, Domain.Project.Project>();
            CreateMap<UserDto, Teammate>();
            CreateMap<UpdateUserDto, Teammate>();
            CreateMap<UpdateProjectDto, Domain.Project.Project>();
            CreateMap<RoleDto, ProjectRole>();
            CreateMap<UpdateRoleDto, ProjectRole>();
            CreateMap<BlockDto, Block>();
        }
    }
}
