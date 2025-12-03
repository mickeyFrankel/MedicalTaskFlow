using FluentAssertions;
using MedicalTaskFlow.Core.Interfaces;
using MedicalTaskFlow.Core.Models;
using MedicalTaskFlow.Core.Services;
using Moq;

namespace MedicalTaskFlow.Tests.Unit.Services;
///<summary>
/// Unit tests for TaskService class.
/// Tests business logic in isolation using mocked ITaskRepository.
/// </summary>
public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockRepository;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        _mockRepository = new Mock<ITaskRepository>();
        _taskService = new TaskService(_mockRepository.Object);
    }

    [Fact]
    public async Task CreateTaskAsync_WithValidTask_shouldSetCreatedAt()
    {
        //Arrange
        var task = new TaskItem
        {
            Title = "Test Task",
            Priority = TaskItem.TaskPriority.High
        };
        _mockRepository
        .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
        .ReturnsAsync(task);

        //Act
        var result = await _taskService.CreateTaskAsync(task);

        // Assert
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        _mockRepository.Verify(r => r.AddAsync(task), Times.Once);
    }
    [Fact]
    public async Task CreateTaskAsync_WithNullTask_ShouldThrowArgumentNullException()
    {
        //Act
        Func<Task> act = async() => await _taskService.CreateTaskAsync(null!);

        //Assert
        await act.Should().ThrowAsync<ArgumentNullException>().WithParameterName("task");
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task CreateTaskAsync_WithEmptyTitle_ShouldThrowArgumentException(string title)
    {
        //Arrange
        var task = new TaskItem {Title = title};

        //Act
        Func<Task> act = async() => await _taskService.CreateTaskAsync(task);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Task title cannot be empty*").WithParameterName("Title");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task GetTaskByIdAsync_WithInvalidId_ShouldThrowArgumentException(int id)
    {
        //Act
        Func<Task> act = async() => await _taskService.GetTaskByIdAsync(id);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Task ID must be positive*").WithParameterName(nameof(id));
    }

    [Fact]
    public async Task GetOverdueTasksAsync_ShouldReturnOnlyOverdueTasks()
    {
        // Arange
        var tasks = new List<TaskItem>
        {
            new() { Id = 1, Title = "Overdue", DueDate = DateTime.UtcNow.AddDays(-1), Status = TaskItem.TaskStatus.Todo},
            new() { Id = 2, Title = "Not Due", DueDate = DateTime.UtcNow.AddDays(1), Status = TaskItem.TaskStatus.Todo},
            new() { Id = 3, Title = "Completed", DueDate = DateTime.UtcNow.AddDays(-1), Status = TaskItem.TaskStatus.Completed}
        };

        _mockRepository.Setup( r => r.GetAllAsync()).ReturnsAsync(tasks);

        // Act
        var result = await _taskService.GetOverdueTasksAsync();

        // Assert
        result.Should().HaveCount(1);
        result.First().Title.Should().Be("Overdue");
    }
}