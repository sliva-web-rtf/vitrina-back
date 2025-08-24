using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Content;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;
using Vitrina.UseCases.ProjectPage.Dto;
using Vitrina.UseCases.ProjectPage.UpdateProjectPage;

namespace Vitrina.UseCases.Tests.ProjectPages;

[TestFixture]
public class UpdateProjectPageTests
{
    private IProjectPageRepository repository;
    private IValidator<ContentBlockDto> validator;
    private IMapper mapper;
    private IAppDbContext dbContext;
    private UpdateProjectPageCommandHandler handler;
    private Domain.Project.Page.ProjectPage page;

    private const int UserId = 123;

    [SetUp]
    public void SetUp()
    {
        repository = A.Fake<IProjectPageRepository>();
        validator = A.Fake<IValidator<ContentBlockDto>>();
        mapper = A.Fake<IMapper>();
        dbContext = A.Fake<IAppDbContext>();

        handler = new UpdateProjectPageCommandHandler(repository, validator, mapper, dbContext);

        page = new Domain.Project.Page.ProjectPage
        {
            Id = Guid.NewGuid(),
            ReadyStatus = PageReadyStatusEnum.Draft,
            Editors = new List<PageEditor> { new PageEditor { UserId = UserId, PageId = default, Id = default } }
        };

        var contentBlock =
            new ContentBlock
            {
                Id = Guid.NewGuid(),
                PageId = default,
                Page = null,
                Content = null,
                ContentType = ContentTypeEnum.BlockWithTextAndImage,
                NumberOnPage = 0
            };

        page.ContentBlocks.Clear();
        page.ContentBlocks.Add(contentBlock);

        A.CallTo(() => repository.GetByIdAsync(page.Id, A<CancellationToken>._))
            .Returns(page);

        A.CallTo(() => validator.ValidateAsync(A<ContentBlockDto>._, A<CancellationToken>._))
            .Returns(new ValidationResult());
    }

    [Test]
    public async Task ShouldApplyPatch_AndSaveChanges()
    {
        var patchDoc = new JsonPatchDocument<UpdateProjectPageDto>();
        patchDoc.Replace(p => p.ReadyStatus, PageReadyStatusEnum.Published);

        var pageDto = new UpdateProjectPageDto
        {
            ContentBlocks = new List<ContentBlockDto>
            {
                new ContentBlockDto
                {
                    Id = page.ContentBlocks.First()
                        .Id,
                    Content = null,
                    ContentType = ContentTypeEnum.BlockWithTextAndImage
                }
            },
            ReadyStatus = PageReadyStatusEnum.Draft
        };

        A.CallTo(() => mapper.Map<UpdateProjectPageDto>(page)).Returns(pageDto);
        A.CallTo(() => mapper.Map<ContentBlock>(A<object>.Ignored))
            .Returns(new ContentBlock
            {
                Id = Guid.NewGuid(),
                PageId = default,
                Page = null,
                Content = null,
                ContentType = ContentTypeEnum.BlockWithTextAndImage,
                NumberOnPage = 0
            });

        var command = new UpdateProjectPageCommand(page.Id, patchDoc, UserId);

        await handler.Handle(command, CancellationToken.None);

        A.CallTo(() => dbContext.SaveChangesAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task ShouldThrow_WhenValidationFails()
    {
        var patchDoc = new JsonPatchDocument<UpdateProjectPageDto>();
        var pageDto = new UpdateProjectPageDto
        {
            ContentBlocks = new List<ContentBlockDto>
            {
                new ContentBlockDto
                {
                    Id = Guid.NewGuid(),
                    Content = null,
                    ContentType = ContentTypeEnum.BlockWithTextAndImage
                }
            },
            ReadyStatus = PageReadyStatusEnum.Draft
        };

        A.CallTo(() => mapper.Map<UpdateProjectPageDto>(page)).Returns(pageDto);
        A.CallTo(() => validator.ValidateAsync(A<ContentBlockDto>._, A<CancellationToken>._))
            .Returns(new ValidationResult(new[] { new ValidationFailure("Test", "Invalid") }));

        var command = new UpdateProjectPageCommand(page.Id, patchDoc, UserId);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Handle_ShouldAddNewBlock_WhenItDoesNotExist()
    {
        var patchDoc = new JsonPatchDocument<UpdateProjectPageDto>();
        var newBlockDto = new ContentBlockDto
        {
            Id = Guid.NewGuid(), Content = null, ContentType = ContentTypeEnum.BlockWithTextAndImage
        };

        var pageDto = new UpdateProjectPageDto
        {
            ContentBlocks = new List<ContentBlockDto> { newBlockDto }, ReadyStatus = PageReadyStatusEnum.Draft
        };

        A.CallTo(() => mapper.Map<UpdateProjectPageDto>(page)).Returns(pageDto);
        A.CallTo(() => mapper.Map<ContentBlock>(newBlockDto))
            .Returns(new ContentBlock
            {
                Id = newBlockDto.Id,
                PageId = default,
                Page = null,
                Content = null,
                ContentType = ContentTypeEnum.BlockWithTextAndImage,
                NumberOnPage = 0
            });

        var command = new UpdateProjectPageCommand(page.Id, patchDoc, UserId);

        await handler.Handle(command, CancellationToken.None);

        A.CallTo(() => dbContext.ContentBlocks.Add(A<ContentBlock>.That.Matches(b => b.Id == newBlockDto.Id)))
            .MustHaveHappenedOnceExactly();
    }

    [TearDown]
    public void TearDown()
    {
        if (dbContext is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
