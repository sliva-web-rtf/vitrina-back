using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.DeleteEditorByPageEditorId;
using Vitrina.UseCases.ProjectPage.Dto;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page.Editor;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class DeleteEditorByPageEditorIdTests
{
    private IPageEditorRepository editorRepository;
    private IProjectPageRepository pageRepository;
    private IMapper mapper;
    private DeleteEditorByPageEditorIdCommandHandler handler;

    private readonly Guid PageId = Guid.NewGuid();
    private readonly Guid EditorId = Guid.NewGuid();
    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        editorRepository = A.Fake<IPageEditorRepository>();
        pageRepository = A.Fake<IProjectPageRepository>();
        mapper = A.Fake<IMapper>();

        handler = new DeleteEditorByPageEditorIdCommandHandler(editorRepository, pageRepository, mapper);
    }

    [Test]
    public async Task ShouldDeleteEditor_AndReturnMappedDto()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            Id = default
        };
        var deletedEditor = new PageEditor
        {
            Id = EditorId,
            PageId = PageId,
            User = new Domain.User.User
            {
                Id = 1,
                Email = "user@example.com",
                FirstName = null,
                LastName = null
            },
            UserId = UserId
        };

        var existingEditor = new PageEditor
        {
            Id = Guid.NewGuid(), User = deletedEditor.User, PageId = page.Id, UserId = UserId
        };

        page.Editors.Add(existingEditor);


        A.CallTo(() => pageRepository.GetByIdAsync(PageId, A<CancellationToken>._)).Returns(page);
        A.CallTo(() => editorRepository.DeleteAsync(EditorId, PageId, A<CancellationToken>._)).Returns(deletedEditor);

        var expectedDto = new PageEditorDto
        {
            Id = EditorId,
            User = null,
            Status = EditorStatus.Creator
        };
        A.CallTo(() => mapper.Map<PageEditorDto>(deletedEditor)).Returns(expectedDto);

        var result = await handler.Handle(new DeleteEditorByPageEditorIdCommand(PageId, EditorId, UserId), CancellationToken.None);

        result.Should().Be(expectedDto);
    }

    [Test]
    public void ShouldThrowForbiddenException_WhenUserHasNoAccessRights()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            Id = default
        };

        A.CallTo(() => pageRepository.GetByIdAsync(PageId, A<CancellationToken>._)).Returns(page);

        var act = async () => await handler.Handle(
            new DeleteEditorByPageEditorIdCommand(PageId, EditorId, UserId),
            CancellationToken.None);

        act.Should().ThrowAsync<ForbiddenException>();
    }

    [Test]
    public void ShouldThrow_WhenEditorRepositoryReturnsNull()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            Id = default
        };
        A.CallTo(() => pageRepository.GetByIdAsync(PageId, A<CancellationToken>._)).Returns(page);
        A.CallTo(() => editorRepository.DeleteAsync(EditorId, PageId, A<CancellationToken>._)).Returns((PageEditor)null);

        var act = async () => await handler.Handle(
            new DeleteEditorByPageEditorIdCommand(PageId, EditorId, UserId),
            CancellationToken.None);

        act.Should().ThrowAsync<NullReferenceException>();
    }
}
