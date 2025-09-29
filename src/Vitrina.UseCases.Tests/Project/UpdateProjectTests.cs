using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.DataAccess;
using Vitrina.UseCases.Project.Dto;
using Vitrina.UseCases.Project.UpdateProject;

namespace Vitrina.UseCases.Tests.Project;

[TestFixture]
public class UpdateProjectTests
{
    private IAppDbContext dbContext;
    private IMapper mapper;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMemoryDb_Update")
            .Options;

        dbContext = new AppDbContext(options);

        dbContext.Projects.RemoveRange(dbContext.Projects);
        dbContext.SaveChangesAsync();

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Domain.Project.Project, UpdateProjectDto>().ReverseMap();
            cfg.CreateMap<Domain.Project.Project, ResponceProjectDto>();
        });
        mapper = configuration.CreateMapper();
    }

    [Test]
    public async Task ProjectExists_UpdatesProject()
    {
        var project = new Domain.Project.Project
        {
            Id = 1, Name = "Old Name", PageId = default, Page = null, CreatorId = 1
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var patchDoc = new JsonPatchDocument<UpdateProjectDto>();
        patchDoc.Replace(p => p.Name, "New Name");

        var handler = new UpdateProjectCommandHandler(mapper, dbContext);
        var command = new UpdateProjectCommand(1, patchDoc, 1);

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("New Name");

        var updatedProject = await dbContext.Projects.FindAsync(1);
        updatedProject.Name.Should().Be("New Name");
    }

    [Test]
    public void ProjectDoesNotExist_ThrowsNotFoundException()
    {
        var patchDoc = new JsonPatchDocument<UpdateProjectDto>();
        patchDoc.Replace(p => p.Name, "New Name");

        var handler = new UpdateProjectCommandHandler(mapper, dbContext);
        var command = new UpdateProjectCommand(999, patchDoc, 1);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public void Handle_NoAccessRights_ThrowsException()
    {
        var project = new Domain.Project.Project
        {
            Id = 2, Name = "Private Project", PageId = default, Page = null, CreatorId = 1
        };
        dbContext.Projects.Add(project);
        dbContext.SaveChangesAsync();

        var patchDoc = new JsonPatchDocument<UpdateProjectDto>();
        patchDoc.Replace(p => p.Name, "Hacked Name");

        var handler = new UpdateProjectCommandHandler(mapper, dbContext);
        var command = new UpdateProjectCommand(2, patchDoc, 999);

        var act = async () => await handler.Handle(command, CancellationToken.None);

        act.Should().ThrowAsync<DomainException>();
    }

    [TearDown]
    public void TearDown()
    {
        dbContext.Dispose();
    }
}
