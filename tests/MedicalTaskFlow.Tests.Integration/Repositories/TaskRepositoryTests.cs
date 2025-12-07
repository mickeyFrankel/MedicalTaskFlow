using FluentAssertions;
using MedicalTaskFlow.Core.Models;
using MedicalTaskFlow.Data.Repositories;

namespace MedicalTaskFlow.Tests.Integration.Repositories;
///<summary>
/// Integration tests for TaskRepository.
/// Tests actual database operations using in-memory SQLite.
///</summary>
public class TaskRepositoryTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private const string TITLE = "Integration Test Task";

    public TaskRepositoryTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public async Task AddAsync_ShouldPersistTaskToDatabase()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var repository = new TaskRepository(context);
        var task = new TaskItem
        {
            Title = TITLE,
            Description = "Testing repository integration",
            Priority = TaskItem.TaskPriority.High,
            Status = TaskItem.TaskStatus.Todo
        };

        // Act
        var result = await repository.AddAsync(task);


        // Assert
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be(TITLE);

        // Verify persistence
        var retrievedTask = await repository.GetByIdAsync(result.Id);
        retrievedTask.Should().NotBeNull();
        retrievedTask!.Title.Should().Be(TITLE);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTasks()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var repository = new TaskRepository(context);

        await repository.AddAsync(new TaskItem{Title = "Task 1"});
        await repository.AddAsync(new TaskItem { Title = "Task 2"});
        await repository.AddAsync(new TaskItem { Title = "Task 3"});

        // Act
        var tasks = await repository.GetAllAsync();

        // Assert
        tasks.Should().HaveCount(3);
        tasks.Select(t=>t.Title).Should().Contain(new[] {"Task 1", "Task 2", "Task 3"});
        }

        [Fact]
        public async Task UpdateAsync_ShouldModiftExistingTask()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var repository = new TaskRepository(context);

        var task = await repository.AddAsync(new TaskItem{Title = "Original"});

        // Act
        task.Title = "Updated";
        task.Status = TaskItem.TaskStatus.Completed;
        await repository.UpdateAsync(task);

        // Assert
        var updatedTask = await repository.GetByIdAsync(task.Id);
        updatedTask!.Title.Should().Be("Updated");
        updatedTask.Status.Should().Be(TaskItem.TaskStatus.Completed);
    }
    [Fact]
    public async Task DeleteAsync_ShouldRemoveTaskFromDatabase()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var repository = new TaskRepository(context);

        var task = await repository.AddAsync(new TaskItem {Title = "To Be Deleted"});

        // Act
        var result = await repository.DeleteAsync(task.Id);

        // Assert
        result.Should().BeTrue();
        var deletedTask = await repository.GetByIdAsync(task.Id);
        deletedTask.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_WithNonExistentId_ShouldReturnFalse()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var repository = new TaskRepository(context);

        // Act
        var result = await repository.DeleteAsync(9999); // Assuming this ID doesn't exist

        // Assert
        result.Should().BeFalse();
    }

    
    }
