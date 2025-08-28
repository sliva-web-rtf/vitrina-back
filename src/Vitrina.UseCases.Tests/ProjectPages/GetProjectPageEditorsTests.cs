using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPage.GetProjectPageEditors;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class GetProjectPageEditorsTests
{
    private IProjectPageRepository repository;
    private IMapper mapper;
    private UserManager<Domain.User.User> userManager;
    private GetProjectPageEditorsQueryHandler handler;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        repository = A.Fake<IProjectPageRepository>();
        mapper = A.Fake<IMapper>();
        userManager = A.Fake<UserManager<Domain.User.User>>(
            o => o.WithArgumentsForConstructor(
                () => new UserManager<Domain.User.User>(
                    A.Fake<IUserStore<Domain.User.User>>(),
                    null, null, null, null, null, null, null, null)));

        handler = new GetProjectPageEditorsQueryHandler(repository, mapper, userManager);
    }

    [Test]
    public async Task WhenPageIsPublished_ShouldReturnMappedEditors()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Published,
            Editors = new List<PageEditor>
            {
                new PageEditor
                {
                    Id = Guid.NewGuid(),
                    UserId = 0,
                    PageId = default
                }
            },
            Id = default
        };

        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);

        var expectedDtos = new List<PageEditorDto> { new PageEditorDto
            {
                Id = page.Editors.First()
                    .Id,
                User = null,
                Status = EditorStatus.Creator
            }
        };
        A.CallTo(() => mapper.Map<ICollection<PageEditorDto>>(page.Editors)).Returns(expectedDtos);

        var result = await handler.Handle(new GetProjectPageEditorsQuery(Guid.NewGuid(), null), CancellationToken.None);

        result.Should().BeEquivalentTo(expectedDtos);
    }

    [Test]
    public async Task WhenPageIsNotPublished_AndUserIsAdmin_ShouldReturnMappedEditors()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            Editors = new List<PageEditor>
            {
                new PageEditor
                {
                    Id = Guid.NewGuid(),
                    UserId = 0,
                    PageId = default
                }
            },
            Id = default
        };
        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);

        var adminUser = new Domain.User.User
        {
            RoleOnPlatform = RoleOnPlatformEnum.Administrator,
            FirstName = null,
            LastName = null,
            Email = null
        };
        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns(adminUser);

        var expectedDtos = new List<PageEditorDto> { new PageEditorDto
            {
                Id = page.Editors.First()
                    .Id,
                User = null,
                Status = EditorStatus.Creator
            }
        };
        A.CallTo(() => mapper.Map<ICollection<PageEditorDto>>(page.Editors)).Returns(expectedDtos);

        var result = await handler.Handle(new GetProjectPageEditorsQuery(Guid.NewGuid(), UserId), CancellationToken.None);

        result.Should().BeEquivalentTo(expectedDtos);
    }

    [Test]
    public void WhenPageIsNotPublished_AndUserIsNotAdmin_ShouldThrowForbiddenException()
    {
        var page = new Domain.Project.Page.ProjectPage
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            Editors = new List<PageEditor>
            {
                new PageEditor
                {
                    Id = Guid.NewGuid(),
                    UserId = 0,
                    PageId = default
                }
            },
            Id = default
        };
        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._)).Returns(page);

        var normalUser = new Domain.User.User
        {
            RoleOnPlatform = RoleOnPlatformEnum.Student,
            FirstName = null,
            LastName = null,
            Email = null
        };
        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns(normalUser);

        var act = async () =>
            await handler.Handle(new GetProjectPageEditorsQuery(Guid.NewGuid(), UserId), CancellationToken.None);

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
