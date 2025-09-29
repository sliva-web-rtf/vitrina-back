using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.Project.GetProjectById;

namespace Vitrina.UseCases.Tests.Project;

[TestFixture]
public class GetProjectByIdTests
{
    private IMapper mapper;
    private IAppDbContext dbContext;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

        dbContext = new AppDbContext(options);

        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.SaveChangesAsync();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Project.Project, ResponceProjectDto>();
        });
        mapper = configuration.CreateMapper();
    }

    [Test]
    public async Task ProjectExists_ReturnsProjectDto()
    {
        var project = new Domain.Project.Project
        {
            Id = 1, Name = "Test Project", PageId = default, Page = null, CreatorId = 0
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var handler = new GetProjectByIdQueryHandler(mapper, dbContext);

        var result = await handler.Handle(new GetProjectByIdQuery(1), CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Test Project");
    }

    [Test]
    public void ProjectDoesNotExist_ThrowsNotFoundException()
    {
        var handler = new GetProjectByIdQueryHandler(mapper, dbContext);

        var act = async () => await handler.Handle(new GetProjectByIdQuery(999), CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }
}
