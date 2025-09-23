using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPage.GetProjectPage;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class GetProjectPageByIdTests
{
    private IProjectPageRepository repository;
    private IMapper mapper;
    private IAppDbContext dbContext;
    private UserManager<Domain.User.User> userManager;
    private GetProjectPageByIdQueryHandler handler;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        repository = A.Fake<IProjectPageRepository>();
        mapper = A.Fake<IMapper>();
        dbContext = A.Fake<IAppDbContext>();
        userManager = A.Fake<UserManager<Domain.User.User>>(
            o => o.WithArgumentsForConstructor(
                () => new UserManager<Domain.User.User>(
                    A.Fake<IUserStore<Domain.User.User>>(),
                    null, null, null, null, null, null, null, null)));

        handler = new GetProjectPageByIdQueryHandler(repository, mapper, userManager, dbContext);
    }

    [Test]
    public async Task WhenPageIsPublished_ShouldReturnMappedDto()
    {
        var page = A.Fake<Domain.Project.Page.ProjectPage>();
        page.ReadyStatus = PageReadyStatusEnum.Published;
        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .Returns(page);

        var expectedDto = new ResponceProjectPageDto
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            ContentBlocks = null,
            Id = default
        };
        A.CallTo(() => mapper.Map<ResponceProjectPageDto>(page)).Returns(expectedDto);

        var result = await handler.Handle(
            new GetProjectPageByIdQuery(Guid.NewGuid(), null), CancellationToken.None);

        result.Should().Be(expectedDto);
    }

    [Test]
    public async Task WhenPageIsNotPublished_AndUserIsAdmin_ShouldReturnMappedDto()
    {
        var page = A.Fake<Domain.Project.Page.ProjectPage>();
        page.ReadyStatus = PageReadyStatusEnum.Draft;
        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .Returns(page);

        var adminUser = new Domain.User.User
        {
            RoleOnPlatform = RoleOnPlatformEnum.Administrator,
            FirstName = null,
            LastName = null,
            Email = null
        };
        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns(adminUser);

        var expectedDto = new ResponceProjectPageDto
        {
            ReadyStatus = PageReadyStatusEnum.Draft,
            ContentBlocks = null,
            Id = default
        };
        A.CallTo(() => mapper.Map<ResponceProjectPageDto>(page)).Returns(expectedDto);

        var result = await handler.Handle(
            new GetProjectPageByIdQuery(Guid.NewGuid(), UserId), CancellationToken.None);

        result.Should().Be(expectedDto);
    }

    [Test]
    public void WhenPageIsNotPublished_AndUserIsNotAdmin_ShouldCheckAccessRights()
    {
        var page = A.Fake<Domain.Project.Page.ProjectPage>();
        page.ReadyStatus = PageReadyStatusEnum.Draft;
        A.CallTo(() => repository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .Returns(page);

        var normalUser = new Domain.User.User
        {
            RoleOnPlatform = RoleOnPlatformEnum.Student,
            FirstName = null,
            LastName = null,
            Email = null
        };
        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns(normalUser);

        var act = async () => await handler.Handle(
            new GetProjectPageByIdQuery(Guid.NewGuid(), UserId), CancellationToken.None);

        act.Should().ThrowAsync<ForbiddenException>();
    }

    [TearDown]
    public void TearDown()
    {
        if (userManager is IDisposable disposable)
        {
            disposable.Dispose();
        }

        if (dbContext is IDisposable dbContextIsDisposable)
        {
            dbContextIsDisposable.Dispose();
        }
    }
}
