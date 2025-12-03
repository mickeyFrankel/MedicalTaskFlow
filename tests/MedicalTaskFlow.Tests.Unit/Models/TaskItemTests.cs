using FluentAssertions;
using MedicalTaskFlow.Core.Models;

namespace MedicalTaskFlow.Tests.Unit.Models;
///<summary>
/// 
/// 
/// </summary>
public class TaskItemTests
{
    [Fact]
    public void MarkAsComplete_ShouldSetStatusToComplete()
    {
        // Arrange
        var task = new TaskItem {Title = "Test"};

        // Act
        task.MarkAsComplete();

        // Assert
        task.Status.Should().Be(TaskItem.TaskStatus.Completed);
        task.CompletedAt.Should().NotBeNull();
        task.CompletedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void IsOverdue_WithPastDueDateAndNotCompleted_ShouldReturnTrue()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = "Overdue Task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            Status = TaskItem.TaskStatus.InProgress
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        isOverdue.Should().BeTrue();
    }

    [Fact]
    public void IsOverdue_WithCompletedTask_ShoulReturnFalse()
    {
        // Arrange
        var task = new TaskItem
        {
            Title = "Completed Task",
            DueDate = DateTime.UtcNow.AddDays(-1),
            Status = TaskItem.TaskStatus.Completed
        };

        // Act
        var isOverdue = task.IsOverdue();

        // Assert
        isOverdue.Should().BeFalse();
    }
}