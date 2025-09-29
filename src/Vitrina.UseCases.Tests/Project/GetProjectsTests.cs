using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.Project.GetProjects;

namespace Vitrina.UseCases.Tests.Project;

[TestFixture]
public class GetProjectsTests
{
    private IAppDbContext dbContext;
    private IMapper mapper;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"InMemoryDb_{Guid.NewGuid()}")
            .Options;

        dbContext = new AppDbContext(options);

        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.SaveChangesAsync();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Project.Project, ResponceProjectDto>();

            cfg.CreateMap<Domain.Project.ProjectSphere, ProjectSphere.RequestSphereDto>();
            cfg.CreateMap<Domain.Project.ProjectThematics, ProjectThematics.RequestThematicsDto>();

            cfg.CreateMap<Domain.Project.Page.ProjectPage, ResponceProjectDto>();
        });

        mapper = configuration.CreateMapper();
    }

    [Test]
    public async Task ReturnsOnlyPublishedProjects()
    {
        dbContext.Projects.Add(new Domain.Project.Project
        {
            Id = 1,
            Name = "Published",
            Page = new Domain.Project.Page.ProjectPage
            {
                ReadyStatus = PageReadyStatusEnum.Published, Id = default
            },
            Priority = 1,
            PageId = default,
            CreatorId = 0
        });
        dbContext.Projects.Add(new Domain.Project.Project
        {
            Id = 2,
            Name = "Draft",
            Page = new Domain.Project.Page.ProjectPage { ReadyStatus = PageReadyStatusEnum.Draft, Id = default },
            Priority = 2,
            PageId = default,
            CreatorId = 0
        });
        await dbContext.SaveChangesAsync();

        var handler = new GetProjectsQueryHandler(mapper, dbContext);
        var query = new GetProjectsQuery { Page = 1, PageSize = 10 };

        var result = await handler.Handle(query, CancellationToken.None);
        result.PageSize.Should().Be(10);
        result.First().Name.Should().Be("Published");
    }

    [Test]
    public async Task FiltersByNameClientSphereThematics()
    {
        dbContext.Projects.Add(new Domain.Project.Project
        {
            Id = 1,
            Name = "Project A",
            Client = "Client X",
            Sphere = new Domain.Project.ProjectSphere { Name = "IT", Id = default },
            Thematics = new Domain.Project.ProjectThematics { Name = "AI", Id = default },
            Page = new Domain.Project.Page.ProjectPage
            {
                ReadyStatus = PageReadyStatusEnum.Published, Id = default
            },
            Priority = 1,
            PageId = default,
            CreatorId = 0
        });
        dbContext.Projects.Add(new Domain.Project.Project
        {
            Id = 2,
            Name = "Project B",
            Client = "Client Y",
            Sphere = new Domain.Project.ProjectSphere { Name = "Finance", Id = default },
            Thematics = new Domain.Project.ProjectThematics { Name = "Blockchain", Id = default },
            Page = new Domain.Project.Page.ProjectPage
            {
                ReadyStatus = PageReadyStatusEnum.Published, Id = default
            },
            Priority = 2,
            PageId = default,
            CreatorId = 0
        });

        await dbContext.SaveChangesAsync();

        var handler = new GetProjectsQueryHandler(mapper, dbContext);
        var query = new GetProjectsQuery
        {
            Name = "Project A",
            Client = "Client X",
            Sphere = "IT",
            Thematics = "AI",
            Page = 1,
            PageSize = 10
        };

        var result = await handler.Handle(query, CancellationToken.None);

        result.PageSize.Should().Be(10);
        var project = result.First();
        project.Name.Should().Be("Project A");
        project.Client.Should().Be("Client X");
    }

    [Test]
    public async Task ReturnsPagedResults()
    {
        for (var i = 1; i <= 25; i++)
        {
            dbContext.Projects.Add(new Domain.Project.Project
            {
                Id = i,
                Name = $"Project {i}",
                Page = new Domain.Project.Page.ProjectPage
                {
                    ReadyStatus = PageReadyStatusEnum.Published, Id = default
                },
                Priority = i,
                PageId = default,
                CreatorId = 0
            });
        }
        await dbContext.SaveChangesAsync();

        var handler = new GetProjectsQueryHandler(mapper, dbContext);
        var query = new GetProjectsQuery { Page = 2, PageSize = 10 };

        var result = await handler.Handle(query, CancellationToken.None);

        result.Page.Should().Be(2);
        result.PageSize.Should().Be(10);
        result.TotalCount.Should().Be(25);
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }
}
