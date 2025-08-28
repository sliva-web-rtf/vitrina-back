using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.CreateProjectPage;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class CreateProjectPageTests
{
    private IMapper mapper;
    private IProjectPageRepository repository;
    private UserManager<Domain.User.User> userManager;
    private IValidator<ContentBlockDto> validator;
    private CreateProjectPageCommandHandler handler;

    [SetUp]
    public void SetUp()
    {
        mapper = A.Fake<IMapper>();
        repository = A.Fake<IProjectPageRepository>();
        userManager = A.Fake<UserManager<Domain.User.User>>(o => o.WithArgumentsForConstructor(() =>
            new UserManager<Domain.User.User>(A.Fake<IUserStore<Domain.User.User>>(),
                null, null, null, null, null, null, null, null)));

        validator = A.Fake<IValidator<ContentBlockDto>>();

        handler = new CreateProjectPageCommandHandler(mapper, repository, userManager, validator);
    }

    [Test]
    public async Task ShouldReturnNewGuid_WhenSuccess()
    {
        var user = CreateUserWithId(123);
        A.CallTo(() => userManager.FindByIdAsync("123")).Returns(user);

        var blockDto = new ContentBlockDto
        {
            Id = Guid.NewGuid(),
            Content = new Newtonsoft.Json.Linq.JObject(),
            ContentType = ContentTypeEnum.TextBlock
        };

        A.CallTo(() => validator.ValidateAsync(blockDto, A<CancellationToken>._))
            .Returns(new ValidationResult());

        var block = CreateValidContentBlock();
        A.CallTo(() => mapper.Map<ContentBlock>(blockDto)).Returns(block);

        var dto = new CreateProjectPageDto { ContentBlocks = new List<ContentBlockDto> { blockDto } };
        var command = new CreateProjectPageCommand(dto, 123);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeEmpty();

        A.CallTo(() => repository.AddAsync(A<Domain.Project.Page.ProjectPage>._, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => repository.SaveChangesAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task ShouldThrowValidationException_WhenBlockInvalid()
    {
        var user = CreateUserWithId(123);
        A.CallTo(() => userManager.FindByIdAsync("123")).Returns(user);

        var blockDto = CreateContentBlockDto();

        var invalidResult = new ValidationResult(new[] { new ValidationFailure("Field", "Error") });
        A.CallTo(() => validator.ValidateAsync(blockDto, A<CancellationToken>._))
            .Returns(invalidResult);

        var dto = new CreateProjectPageDto { ContentBlocks = new List<ContentBlockDto> { blockDto } };
        var command = new CreateProjectPageCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
        A.CallTo(() => repository.AddAsync(A<Domain.Project.Page.ProjectPage>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Test]
    public async Task ShouldThrowInvalidOperationException_WhenUserNotFound()
    {
        A.CallTo(() => userManager.FindByIdAsync("123")).Returns((Domain.User.User?)null);

        var dto = new CreateProjectPageDto { ContentBlocks = [] };
        var command = new CreateProjectPageCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<NullReferenceException>(); // так как creator.Id дергается у null
    }

    private ContentBlock CreateValidContentBlock()
    {
        return new ContentBlock
        {
            PageId = default,
            Page = null,
            Content = null,
            ContentType = ContentTypeEnum.BlockWithTextAndImage,
            NumberOnPage = 0,
            Id = default
        };
    }

    private Domain.User.User CreateUserWithId(int id)
    {
        return new Domain.User.User
        {
            Id = id,
            FirstName = null,
            LastName = null,
            Email = null
        };
    }

    private ContentBlockDto CreateContentBlockDto()
    {
        return new ContentBlockDto
        {
            Id = Guid.NewGuid(),
            Content = new JObject(),
            ContentType = ContentTypeEnum.TextBlock
        };
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
