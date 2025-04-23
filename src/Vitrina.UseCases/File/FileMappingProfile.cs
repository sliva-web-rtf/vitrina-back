using AutoMapper;
using Vitrina.Domain.File;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.File;

public class FileMappingProfile : Profile
{
    public FileMappingProfile()
    {
        CreateMap<Domain.File.File, FileDto>()
            .ForMember(
                cloudFileDto => cloudFileDto.Extension,
                member => member.MapFrom(src => src.Extension.Extension));
    }
}
