using AutoMapper;
using Vitrina.Domain.Project.Page;

namespace Vitrina.UseCases.ProjectPages;

public class ProjectPageMappingProfile : Profile
{
    public ProjectPageMappingProfile()
    {
        CreateMap<ProjectPage, ProjectPageDto>()
            .ForAllMembers(config => config.Ignore());
    }
}
