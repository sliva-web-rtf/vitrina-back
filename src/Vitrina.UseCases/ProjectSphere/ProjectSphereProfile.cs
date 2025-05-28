using AutoMapper;

namespace Vitrina.UseCases.ProjectSphere;

public class ProjectSphereProfile : Profile
{
    public ProjectSphereProfile()
    {
        CreateMap<Domain.Project.ProjectSphere, RequestSphereDto>().ReverseMap();
        CreateMap<Domain.Project.ProjectSphere, ResponceSphereDto>().ReverseMap();
    }
}
