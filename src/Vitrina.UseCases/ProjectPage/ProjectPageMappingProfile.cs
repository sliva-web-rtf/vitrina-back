using AutoMapper;
using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPages;

public class ProjectPageMappingProfile : Profile
{
    public ProjectPageMappingProfile()
    {
        CreateMap<Domain.Project.Page.ProjectPage, ProjectPageDto>()
            .ForMember(
                member => member.CreatorId,
                memberConfiguration => memberConfiguration.MapFrom(
                    page => page.Editors.First(editor => editor.Status == EditorStatus.Creator)))
            .ForAllMembers(member => member.Ignore());
        CreateMap<ProjectPageDto, Domain.Project.Page.ProjectPage>();
        CreateMap<ContentBlock, CodeBlockDto>()
            .ReverseMap()
            .ForAllMembers(member => member.Ignore());
        CreateMap<PageEditor, PageEditorDto>()
            .ReverseMap()
            .ForAllMembers(member => member.Ignore());
        CreateMap<CreateProjectPageCommand, Domain.Project.Page.ProjectPage>()
            .ForAllMembers(member => member.Ignore());
    }
}
