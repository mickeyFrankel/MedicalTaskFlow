using System.Dynamic;

namespace MedicalTaskFlow.Core.Models;
///<summary>
/// Represents a task item in the medical task flow system.
/// Domain model follwing the DDD principles.
///</summary>
 public class TaskItem
{
    public int Id{get; set;}
    public string Title {get; set;} = string.Empty;
    public string? Description {get; set;}
    public TaskPriority Priority {get; set;} = TaskPriority.Medium;
    public TaskStatus Status {get; set;} = TaskStatus.Todo;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime? DueDate {get; set;}
    public DateTime? CompletedAt {get; set;}

    ///<summary>
    /// Marks task as completed, and sets completion tiestamp. 
    /// Perfomrs encapsulating logic in domain model.
    /// </summary>
    public void MarkAsComplete ()
    {
        Status = TaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }
    ///<summary>
    /// Validates if a task is overdue
    /// </summary>
    public bool IsOverdue()
    {
        return DueDate.HasValue && DueDate < DateTime.UtcNow && Status != TaskStatus.Completed;
    }
    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Critical = 3
    }
    public enum TaskStatus
    {
        Todo = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }
}