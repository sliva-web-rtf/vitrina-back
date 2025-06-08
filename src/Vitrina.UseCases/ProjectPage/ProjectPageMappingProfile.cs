using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage;

public class ProjectPageMappingProfile : Profile
{
    public ProjectPageMappingProfile()
    {
        CreateMap<Domain.Project.Page.ProjectPage, ProjectPageDto>();
        CreateMap<ProjectPageDto, Domain.Project.Page.ProjectPage>()
            .ForMember(projectPage => projectPage.Editors, member => member.Ignore())
            .ForMember(projectPage => projectPage.Project, member => member.Ignore());
        CreateMap<ContentBlock, ContentBlockDto>()
            .ForMember(
                contentBlockDto => contentBlockDto.Content,
                member => member.MapFrom(contentBlock => JObject.Parse(contentBlock.Content)));
        CreateMap<ContentBlockDto, ContentBlock>()
            .ForMember(
                contentBlock => contentBlock.Content,
                member => member.MapFrom(contentBlock => contentBlock.Content.ToString(Formatting.Indented)))
            .IgnoreAllNonExisting();
        CreateMap<PageEditor, PageEditorDto>()
            .ReverseMap()
            .ForAllMembers(member => member.Ignore());
        CreateMap<CreateProjectPageCommand, Domain.Project.Page.ProjectPage>()
            .ForAllMembers(member => member.Ignore());
    }
}
