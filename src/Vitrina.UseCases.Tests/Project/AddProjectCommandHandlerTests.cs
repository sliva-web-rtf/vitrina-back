using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Project.AddProject;
using Vitrina.UseCases.Tests.Infrastructure;
using Xunit;

namespace Vitrina.UseCases.Tests.Project;

/// <summary>
/// Tests for AddProjectCommandHandler.
/// </summary>
public class AddProjectCommandHandlerTests
{
    /// <summary>
    /// Проверяет, что обработчик успешно создает проект и возвращает его идентификатор.
    /// </summary>
    [Fact]
    public async Task Handle_ValidProject_ShouldCreateProjectAndReturnId()
    {
        // Arrange
        var mapper = TestMapper.CreateMapper();
        var dbContext = new TestDbContext();

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

        var handler = new AddProjectCommandHandler(mapper, dbContext);

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

    /// <summary>
    /// Проверяет, что обработчик повторно использует существующие роли вместо создания дубликатов.
    /// </summary>
    [Fact]
    public async Task Handle_DuplicateRoles_ShouldReuseExistingRoles()
    {
        // Arrange
        var mapper = TestMapper.CreateMapper();
        var dbContext = new TestDbContext();

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
                    Roles = new List<RoleDto> { new RoleDto { Name = "Developer" } }
                }
            }
        };

        var handler = new AddProjectCommandHandler(mapper, dbContext);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var roleCount = await dbContext.ProjectRoles.CountAsync();
        Assert.Equal(1, roleCount); // Should not create duplicate role
    }
}
