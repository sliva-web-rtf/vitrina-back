using AutoMapper;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.BasicContentUnits;
using Vitrina.Domain.Project.Page.Blocks;
using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;
using Vitrina.UseCases.ProjectPage.Dto.Blocks;
using Vitrina.UseCases.ProjectPages.BasicContentUnits;
using Vitrina.UseCases.ProjectPages.Blocks;
using Vitrina.UseCases.ProjectPages.CreateProjectPage;

namespace Vitrina.UseCases.ProjectPages;

public class ProjectPageMappingProfile : Profile
{
    public ProjectPageMappingProfile()
    {
        ConfigureMapping<Domain.Project.Page.ProjectPage, ProjectPageDto>();
        ConfigureMapping<TextUnit, TextUnitDto>();
        ConfigureMapping<PageEditor, PageEditorDto>();
        CreateMap<CreateProjectPageCommand, Domain.Project.Page.ProjectPage>()
            .ForAllMembers(member => member.Ignore());
        ConfigureContentBlockMapping();
        ConfigureMappingBasicContentUnits();
    }

    public void ConfigureMappingBasicContentUnits()
    {
        ConfigureMapping<TextUnit, TextUnitDto>();
        ConfigureMapping<ImageUnit, ImageUnitDto>();
        ConfigureMapping<UnitWithImageAndText, UnitWithImageAndTextDto>();
    }

    public void ConfigureContentBlockMapping()
    {
        ConfigureMapping<ImageAndText, ImageAndTextDto>();
        CreateMap<CodeBlock, CodeBlockDto>()
            .ForMember(
                codeBlockDto => codeBlockDto.ProgrammingLanguage,
                member => member.MapFrom(codeBlock => codeBlock.ProgrammingLanguage.Name))
            .ReverseMap()
            .ForAllMembers(member => member.Ignore());
        ConfigureMapping<VideoBlock, VideoBlockDto>();
        ConfigureMapping<TextBlock, TextBlockDto>();
        ConfigureMapping<ImageBlock, ImageBlockDto>();
        ConfigureMapping<HorizontalDivider, HorizontalDividerDto>();
        ConfigureMapping<CommandBlock, CommandBlockDto>();
        ConfigureMapping<CarouselImages, CarouselImagesDto>();
    }

    public void ConfigureMapping<TFirst, TSecond>()
    {
        CreateMap<TFirst, TSecond>()
            .ReverseMap()
            .ForAllMembers(member => member.Ignore());
    }
}
