using AutoMapper;

namespace Vitrina.UseCases.ProjectThematics;

public class ProjectThematicsProfile : Profile
{
    public ProjectThematicsProfile()
    {
        CreateMap<Domain.Project.ProjectThematics, RequestThematicsDto>().ReverseMap();
        CreateMap<Domain.Project.ProjectThematics, ResponceThematicsDto>().ReverseMap();
    }
}
