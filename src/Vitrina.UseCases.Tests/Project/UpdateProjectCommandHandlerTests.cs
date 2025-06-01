using Saritasa.Tools.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Project.UpdateProject;
using Vitrina.UseCases.Project.UpdateProject.DTO;
using Vitrina.UseCases.Tests.Infrastructure;
using Xunit;
using Block = Vitrina.Domain.Project.Block;

namespace Vitrina.UseCases.Tests.Project;

/// <summary>
/// Tests for UpdateProjectCommandHandler.
/// </summary>
public class UpdateProjectCommandHandlerTests
{
    private readonly TestDbContext dbContext;
    private readonly UpdateProjectCommandHandler handler;

    public UpdateProjectCommandHandlerTests()
    {
        var mapper = TestMapper.CreateMapper();
        dbContext = new TestDbContext();
        handler = new UpdateProjectCommandHandler(mapper, dbContext);
    }

    /// <summary>
    /// Проверяет, что обработчик успешно обновляет проект с валидными данными.
    /// </summary>
    [Fact]
    public async Task Handle_ValidProject_ShouldUpdateProjectSuccessfully()
    {
        // Arrange
        var existingProject = new Domain.Project.Project
        {
            Name = "Old Name",
            Description = "Old Description",
            Aim = "Old Aim",
            Period = "2023-2024",
            Semester = SemesterEnum.Spring,
            CustomBlocks = new List<Block> { new() { Title = "Old Block", Text = "Old Text", SequenceNumber = 0 } }
        };
        existingProject.Users = new List<Teammate>
        {
            new()
            {
                FirstName = "Old",
                LastName = "User",
                Email = "old.user@example.com",
                Roles = new List<ProjectRole> { new() { Name = "Developer" } },
                Project = existingProject
            }
        };

        await dbContext.Projects.AddAsync(existingProject);
        await dbContext.SaveChangesAsync();

        var updateCommand = new UpdateProjectCommand
        {
            ProjectId = existingProject.Id,
            Project = new UpdateProjectDto
            {
                Name = "New Name",
                Description = "New Description",
                Aim = "New Aim",
                Semester = SemesterEnum.Autumn,
                Users = new List<UpdateUserDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Roles = new List<UpdateRoleDto> { new() { Name = "Lead Developer" } }
                    }
                }
            }
        };

        // Act
        await handler.Handle(updateCommand, CancellationToken.None);

        // Assert
        var updatedProject = await dbContext.Projects
            .Include(p => p.Users)
            .ThenInclude(u => u.Roles)
            .Include(p => p.CustomBlocks)
            .FirstAsync(p => p.Id == existingProject.Id);

        Assert.Equal(updateCommand.Project.Name, updatedProject.Name);
        Assert.Equal(updateCommand.Project.Description, updatedProject.Description);
        Assert.Equal(updateCommand.Project.Aim, updatedProject.Aim);
        Assert.Equal(updateCommand.Project.Semester, updatedProject.Semester);
    }

    /// <summary>
    /// Проверяет, что обработчик выбрасывает исключение при попытке обновить несуществующий проект.
    /// </summary>
    [Fact]
    public async Task Handle_ProjectNotFound_ShouldThrowException()
    {
        // Arrange
        var updateCommand = new UpdateProjectCommand
        {
            ProjectId = 999,
            Project = new UpdateProjectDto
            {
                Name = "New Name",
                Description = "New Description",
                Aim = "New Aim",
                Semester = SemesterEnum.Autumn,
                Users = new List<UpdateUserDto>(),
            }
        };

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => handler.Handle(updateCommand, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ExistingRole_ShouldReuseRole()
    {
        // Arrange
        var existingRole = new ProjectRole { Name = "Developer" };
        await dbContext.ProjectRoles.AddAsync(existingRole);

        var existingProject = new Domain.Project.Project
        {
            Name = "Test Project",
            Description = "Test Description",
            Aim = "Test Aim",
            Period = "2023-2024",
            Semester = SemesterEnum.Spring,
            CustomBlocks = new List<Block>()
        };
        existingProject.Users = new List<Teammate>()
        {
            new() { FirstName = "John", LastName = "Doe", Project = existingProject }
        };
        await dbContext.Projects.AddAsync(existingProject);
        await dbContext.SaveChangesAsync();

        var updateCommand = new UpdateProjectCommand
        {
            ProjectId = existingProject.Id,
            Project = new UpdateProjectDto
            {
                Name = "Updated Project",
                Description = "Updated Description",
                Aim = "Updated Aim",
                Semester = SemesterEnum.Autumn,
                Users = new List<UpdateUserDto>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        Roles = new List<UpdateRoleDto> { new() { Name = "Developer" } }
                    }
                },
            }
        };

        // Act
        await handler.Handle(updateCommand, CancellationToken.None);

        // Assert
        var roleCount = await dbContext.ProjectRoles.CountAsync();
        Assert.Equal(1, roleCount); // Роль не должна была продублироваться
    }
}
