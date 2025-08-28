using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.User.GetUserProjectsPages;

namespace Vitrina.UseCases.Tests.User;

[TestFixture]
public class GetUserProjectPagesByUserIdTests
{
    private UserManager<Domain.User.User> userManager;
    private IMapper mapper;
    private GetUserProjectPagesByUserIdQueyHandler handler;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        userManager = A.Fake<UserManager<Domain.User.User>>(
            o => o.WithArgumentsForConstructor(() => new UserManager<Domain.User.User>(
                A.Fake<IUserStore<Domain.User.User>>(), null, null, null, null, null, null, null, null)));
        mapper = A.Fake<IMapper>();

        handler = new GetUserProjectPagesByUserIdQueyHandler(userManager, mapper);
    }

    [Test]
    public async Task ShouldReturnMappedPages_WhenUserExists()
    {
        var page1 = new Domain.Project.Page.ProjectPage { Id = Guid.NewGuid(), ReadyStatus = PageReadyStatusEnum.Draft };
        var page2 = new Domain.Project.Page.ProjectPage { Id = Guid.NewGuid(), ReadyStatus = PageReadyStatusEnum.Published };
        var user = new Domain.User.User
        {
            Id = UserId,
            EditingRights = new List<PageEditor>
            {
                new PageEditor
                {
                    Page = page1,
                    UserId = UserId,
                    PageId = default,
                    Id = default
                },
                new PageEditor
                {
                    Page = page2,
                    UserId = UserId,
                    PageId = default,
                    Id = default
                }
            },
            FirstName = null,
            LastName = null,
            Email = null
        };

        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns(user);

        var expectedDtos = new List<ResponceProjectPageDto>
        {
            new ResponceProjectPageDto { Id = page1.Id, ReadyStatus = page1.ReadyStatus, ContentBlocks = new List<ContentBlockDto>() },
            new ResponceProjectPageDto { Id = page2.Id, ReadyStatus = page2.ReadyStatus, ContentBlocks = new List<ContentBlockDto>() }
        };
        A.CallTo(() => mapper.Map<ICollection<ResponceProjectPageDto>>(A<IEnumerable<Domain.Project.Page.ProjectPage>>._))
            .Returns(expectedDtos);

        var result = await handler.Handle(new GetUserProjectPagesByUserIdQuey(UserId), CancellationToken.None);

        result.Should().BeEquivalentTo(expectedDtos);
    }

    [Test]
    public void ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        A.CallTo(() => userManager.FindByIdAsync(UserId.ToString())).Returns((Domain.User.User)null);

        var act = async () => await handler.Handle(new GetUserProjectPagesByUserIdQuey(UserId), CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
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
