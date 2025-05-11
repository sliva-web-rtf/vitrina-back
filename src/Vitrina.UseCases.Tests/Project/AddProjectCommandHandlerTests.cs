namespace Vitrina.UseCases.Tests.Project;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Vitrina.Domain.Project;
using Vitrina.Domain.User;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Xunit;
using Block = Vitrina.Domain.Project.Block;

/// <summary>
/// Tests for AddProjectCommandHandler.
/// </summary>
public class AddProjectCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidProject_ShouldCreateProjectAndReturnId()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        var dbContext = new TestDbContext(options);

        var command = new AddProjectCommand
        {
            Name = "Test Project",
            Description = "Test Description",
            Aim = "Test Aim",
            Period = "2024-2025",
            Priority = 1,
            Semester = SemesterEnum.Spring,
            Users = new List<UserDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Roles = new List<RoleDto> { new RoleDto { Name = "Developer" } }
                }
            },
            CustomBlocks = new List<BlockDto>
            {
                new() { Title = "Block 1", Text = "Text 1" }, new() { Title = "Block 2", Text = "Text 2" }
            }
        };
        var project = new Domain.Project.Project
        {
            Name = command.Name,
            Description = command.Description,
            Aim = command.Aim,
            Period = command.Period,
            Semester = command.Semester,
            CustomBlocks = command.CustomBlocks.Select(b => new Block { Title = b.Title, Text = b.Text })
                .ToList()
        };

        project.Users = command.Users.Select(u => new Teammate
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Project = project,
            Roles = new List<ProjectRole> { new() { Name = u.Roles.First().Name } }
        }).ToList();
        mockMapper.Setup(m => m.Map<AddProjectCommand, Domain.Project.Project>(command))
            .Returns(project);

        var handler = new AddProjectCommandHandler(mockMapper.Object, dbContext);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(0, result);
        var savedProject = await dbContext.Projects.Include(p => p.Users).SingleAsync(p => p.Id == result);
        var teammate = savedProject.Users.First();
        Assert.NotNull(savedProject);
        Assert.Equal(command.Name, savedProject.Name);
        Assert.Equal(command.Description, savedProject.Description);
        Assert.Equal(0, savedProject.CustomBlocks.First().SequenceNumber);
        Assert.Equal(1, savedProject.CustomBlocks.Last().SequenceNumber);
        Assert.Single(teammate.Roles);
        Assert.Equal("Developer", teammate.Roles.First().Name);
    }

    [Fact]
    public async Task Handle_DuplicateRoles_ShouldReuseExistingRoles()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_DuplicateRoles")
            .Options;
        var dbContext = new TestDbContext(options);

        // Add existing role
        var existingRole = new ProjectRole { Name = "Developer" };
        dbContext.ProjectRoles.Add(existingRole);
        await dbContext.SaveChangesAsync();

        var command = new AddProjectCommand
        {
            Name = "Test Project",
            Description = "Test Description",
            Aim = "Test Aim",
            Period = "2024-2025",
            Priority = 1,
            Semester = SemesterEnum.Spring,
            Users = new List<UserDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Roles = new List<RoleDto> { new RoleDto { Name = "Developer" } } // Same role name as existing
                }
            }
        };
        var project = new Domain.Project.Project
        {
            Name = command.Name,
            Description = command.Description,
            Aim = command.Aim,
            Period = command.Period,
            Semester = command.Semester,
        };

        project.Users = command.Users.Select(u => new Teammate
        {
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            Project = project,
            Roles = new List<ProjectRole> { new() { Name = u.Roles.First().Name } }
        }).ToList();
        mockMapper.Setup(m => m.Map<AddProjectCommand, Domain.Project.Project>(command))
            .Returns(project);

        var handler = new AddProjectCommandHandler(mockMapper.Object, dbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var roleCount = await dbContext.ProjectRoles.CountAsync();
        Assert.Equal(1, roleCount); // Should not create duplicate role
    }
}

/// <summary>
/// Test database context.
/// </summary>
internal class TestDbContext : DbContext, IAppDbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Teammate> Teammates { get; }

    public DbSet<Domain.Project.Project> Projects { get; set; }

    public DbSet<Tag> Tags { get; }

    public DbSet<ProjectRole> ProjectRoles { get; set; }

    public DbSet<Content> Contents { get; }

    public DbSet<User> Users { get; }

    public DbSet<ConfirmationCode> Codes { get; }
}
