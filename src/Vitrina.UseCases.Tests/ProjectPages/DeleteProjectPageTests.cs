using FakeItEasy;
using FluentAssertions;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.DeleteProjectPage;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class DeleteProjectPageTests
{
    private IProjectPageRepository repository;
    private DeleteProjectPageCommandHandler handler;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        repository = A.Fake<IProjectPageRepository>();
        handler = new DeleteProjectPageCommandHandler(repository);
    }

    [Test]
    public async Task ShouldDeletePage_WhenAccessGrantedAndNoProject()
    {
        var pageId = Guid.NewGuid();
        var page = new Domain.Project.Page.ProjectPage
        {
            ProjectId = null,
            ReadyStatus = PageReadyStatusEnum.Draft,
            Id = default,
            Editors = new List<PageEditor> { new PageEditor { UserId = UserId, PageId = default, Id = default } }
        };

        A.CallTo(() => repository.GetByIdAsync(pageId, A<CancellationToken>._))
            .Returns(page);

        var command = new DeleteProjectPageCommand(pageId, UserId);

        await handler.Handle(command, CancellationToken.None);

        A.CallTo(() => repository.Delete(pageId, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => repository.SaveChangesAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task ShouldThrowDomainException_WhenPageHasProject()
    {
        var pageId = Guid.NewGuid();
        var page = new Domain.Project.Page.ProjectPage
        {
            ProjectId = 1234, ReadyStatus = PageReadyStatusEnum.Draft, Id = default
        };

        A.CallTo(() => repository.GetByIdAsync(pageId, A<CancellationToken>._))
            .Returns(page);

        var command = new DeleteProjectPageCommand(pageId, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public async Task ShouldThrowException_WhenAccessDenied()
    {
        var pageId = Guid.NewGuid();

        var page = new Domain.Project.Page.ProjectPage { ReadyStatus = PageReadyStatusEnum.Draft, Id = default };

        A.CallTo(() => repository.GetByIdAsync(pageId, A<CancellationToken>._))
            .Returns(page);

        var command = new DeleteProjectPageCommand(pageId, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public async Task ShouldThrow_WhenPageNotFound()
    {
        var pageId = Guid.NewGuid();

        A.CallTo(() => repository.GetByIdAsync(pageId, A<CancellationToken>._))
            .Returns<Domain.Project.Page.ProjectPage?>(null);

        var command = new DeleteProjectPageCommand(pageId, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NullReferenceException>();

        A.CallTo(() => repository.Delete(A<Guid>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }
}
