using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.DeleteProject;

namespace Vitrina.UseCases.Tests.Project;

[TestFixture]
public class DeleteProjectTests
{
    private IAppDbContext dbContext;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb_Delete")
            .Options;

        dbContext = new AppDbContext(options);

        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task ProjectExists_DeletesProject()
    {
        var project = new Domain.Project.Project
        {
            Id = 1, Name = "Test Project", PageId = default, Page = null, CreatorId = 0
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var handler = new DeleteProjectCommandHandler(dbContext);
        var command = new DeleteProjectCommand(1, project.CreatorId);

        await handler.Handle(command, CancellationToken.None);

        var deleted = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == 1);
        deleted.Should().BeNull();
    }

    [Test]
    public void ProjectDoesNotExist_ThrowsNotFoundException()
    {
        var handler = new DeleteProjectCommandHandler(dbContext);
        var command = new DeleteProjectCommand(999, 1);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void NoAccessRights_ThrowsException()
    {
        var project = new Domain.Project.Project
        {
            Id = 2, Name = "Private Project", PageId = default, Page = null, CreatorId = 1
        };
        dbContext.Projects.Add(project);
        dbContext.SaveChangesAsync();

        var handler = new DeleteProjectCommandHandler(dbContext);
        var command = new DeleteProjectCommand(2, 999);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<DomainException>();
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }
}
