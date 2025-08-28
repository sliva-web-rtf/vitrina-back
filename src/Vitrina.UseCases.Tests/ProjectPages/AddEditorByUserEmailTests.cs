using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.Common.DTO;
using Vitrina.UseCases.ProjectPage.AddEditorByUserEmail;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class AddEditorByUserEmailTests
{
    private UserManager<Domain.User.User> userManager;
    private IPageEditorRepository editorRepository;
    private IProjectPageRepository pageRepository;
    private IMapper mapper;
    private AddEditorByUserEmailCommandHandler handler;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        userManager = A.Fake<UserManager<Domain.User.User>>(o =>
            o.WithArgumentsForConstructor(() =>
                new UserManager<Domain.User.User>(A.Fake<IUserStore<Domain.User.User>>(),
                    null, null, null, null, null, null, null, null)));

        editorRepository = A.Fake<IPageEditorRepository>();
        pageRepository = A.Fake<IProjectPageRepository>();
        mapper = A.Fake<IMapper>();

        handler = new AddEditorByUserEmailCommandHandler(userManager, editorRepository, pageRepository, mapper);
    }

    [Test]
    public async Task ShouldAddNewEditor_WhenUserExistsAndNotAlreadyAdded()
    {
        var page = new Domain.Project.Page.ProjectPage { ReadyStatus = PageReadyStatusEnum.Draft, Id = default };

        var existingEditorUser = new Domain.User.User
        {
            Id = UserId, Email = "existing@example.com", FirstName = null, LastName = null
        };

        var existingEditor = new PageEditor
        {
            Id = Guid.NewGuid(), User = existingEditorUser, PageId = page.Id, UserId = UserId
        };

        page.Editors.Add(existingEditor);

        var email = "test@example.com";

        A.CallTo(() => pageRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);
        A.CallTo(() => userManager.FindByEmailAsync(email)).Returns(new Domain.User.User
        {
            Id = UserId, FirstName = null, LastName = null, Email = null
        });
        var expectedDto = new PageEditorDto { Id = Guid.NewGuid(), User = null, Status = EditorStatus.Creator };
        A.CallTo(() => mapper.Map<PageEditorDto>(A<PageEditor>._)).Returns(expectedDto);

        var command = new AddEditorByUserEmailCommand(Guid.NewGuid(), new EmailDto { Email = email }, UserId);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().Be(expectedDto);
        A.CallTo(() => editorRepository.AddAsync(A<PageEditor>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => editorRepository.SaveChangesAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ShouldThrowDomainException_WhenUserAlreadyAdded()
    {
        var email = "test@example.com";

        var page = new Domain.Project.Page.ProjectPage { ReadyStatus = PageReadyStatusEnum.Draft, Id = default };

        A.CallTo(() => pageRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);
        var command = new AddEditorByUserEmailCommand(Guid.NewGuid(), new EmailDto { Email = email }, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public void ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        var email = "missing@example.com";
        var page = new Domain.Project.Page.ProjectPage
        {
            Editors = new List<PageEditor>() { new PageEditor { UserId = UserId, PageId = default, Id = default } },
            ReadyStatus = PageReadyStatusEnum.Draft,
            Id = default
        };
        A.CallTo(() => pageRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);
        A.CallTo(() => userManager.FindByEmailAsync(email)).Returns(Task.FromResult<Domain.User.User>(null));

        var command = new AddEditorByUserEmailCommand(Guid.NewGuid(), new EmailDto { Email = email }, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void ShouldThrowForbiddenException_WhenUserHasNoAccessRights()
    {
        var email = "test@example.com";
        var page = A.Fake<Domain.Project.Page.ProjectPage>();
        A.CallTo(() => pageRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);

        var command = new AddEditorByUserEmailCommand(Guid.NewGuid(), new EmailDto { Email = email }, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<ForbiddenException>();
    }

    [TearDown]
    public void TearDown()
    {
        if (userManager is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
