using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.CreateProject;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.ProjectSphere;
using Vitrina.UseCases.ProjectThematics;

namespace Vitrina.UseCases.Tests.Project;

[TestFixture]
public class CreateProjectTests
{
    private IMapper mapper = null!;
    private IAppDbContext dbContext = null!;
    private CreateProjectCommandHandler handler = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        dbContext = context;

        var config = new MapperConfiguration(cfg => { cfg.CreateMap<CreateProjectDto, Domain.Project.Project>(); });
        mapper = config.CreateMapper();

        handler = new CreateProjectCommandHandler(mapper, dbContext);
    }

    [Test]
    public async Task ShouldCreateProject_WhenValidRequest()
    {
        var id = Guid.NewGuid();

        var page = new Domain.Project.Page.ProjectPage
        {
            Id = id,
            ReadyStatus = PageReadyStatusEnum.Draft,
            Editors = new List<PageEditor> { new PageEditor { UserId = 123, PageId = default, Id = default } }
        };
        await dbContext.ProjectPages.AddAsync(page);
        await dbContext.SaveChangesAsync();

        var dto = new CreateProjectDto
        {
            PageId = id, Name = "Test Project", Description = null, PreviewImagePath = null
        };
        var command = new CreateProjectCommand(dto, 123);

        var result = await handler.Handle(command, CancellationToken.None);

        var created = await dbContext.Projects.FindAsync(result);
        created.Should().NotBeNull();
        created!.Name.Should().Be("Test Project");
        created.CreatorId.Should().Be(123);
        created.Page.ReadyStatus.Should().Be(PageReadyStatusEnum.UnderReview);
    }

    [Test]
    public async Task ShouldThrow_WhenProjectAlreadyExistsOnPage()
    {
        var id = Guid.NewGuid();

        var page = new Domain.Project.Page.ProjectPage { Id = id, ReadyStatus = PageReadyStatusEnum.Draft };
        await dbContext.ProjectPages.AddAsync(page);
        await dbContext.Projects.AddAsync(new() { PageId = id, Name = "Proj1", Page = null, CreatorId = 0 });
        await dbContext.SaveChangesAsync();

        var dto = new CreateProjectDto
        {
            PageId = id, Name = "Duplicate Project", Description = null, PreviewImagePath = null
        };
        var command = new CreateProjectCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public async Task ShouldThrow_WhenPageNotFound()
    {
        var id = Guid.NewGuid();

        var dto = new CreateProjectDto
        {
            PageId = id, Name = "Not Found Project", Description = null, PreviewImagePath = null
        };
        var command = new CreateProjectCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public async Task ShouldThrow_WhenSphereNotFound()
    {
        var id = Guid.NewGuid();

        var page = new Domain.Project.Page.ProjectPage { Id = id, ReadyStatus = PageReadyStatusEnum.Draft };
        await dbContext.ProjectPages.AddAsync(page);
        await dbContext.SaveChangesAsync();

        var dto = new CreateProjectDto
        {
            PageId = id,
            Name = "With Sphere",
            Sphere = new RequestSphereDto { Name = "NonExistentSphere" },
            Description = null,
            PreviewImagePath = null
        };
        var command = new CreateProjectCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }

    [Test]
    public async Task ShouldThrow_WhenThematicsNotFound()
    {
        var id = Guid.NewGuid();

        var page = new Domain.Project.Page.ProjectPage { Id = id, ReadyStatus = PageReadyStatusEnum.Draft };
        await dbContext.ProjectPages.AddAsync(page);
        await dbContext.SaveChangesAsync();

        var dto = new CreateProjectDto
        {
            PageId = Guid.NewGuid(),
            Name = "With Thematics",
            Thematics = new RequestThematicsDto() { Name = "NonExistentThematics" },
            Description = null,
            PreviewImagePath = null
        };
        var command = new CreateProjectCommand(dto, 123);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<DomainException>();
    }
}
