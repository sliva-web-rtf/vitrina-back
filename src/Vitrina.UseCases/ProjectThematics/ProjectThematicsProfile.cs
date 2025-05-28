using AutoMapper;

namespace Vitrina.UseCases.ProjectThematics;

public class ProjectThematicsProfile : Profile
{
    public ProjectThematicsProfile()
    {
        CreateMap<Domain.ProjectThematics, RequestThematicsDto>().ReverseMap();
        CreateMap<Domain.ProjectThematics, ResponceThematicsDto>().ReverseMap();
    }
}
