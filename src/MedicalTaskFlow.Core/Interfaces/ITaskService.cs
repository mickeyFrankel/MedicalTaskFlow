using MedicalTaskFlow.Core.Models;
namespace MedicalTaskFlow.Core.Interfaces;
///<summary>
/// Service interface for task operations.
/// Defines the contract for business logic layer
/// </summary>
public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task<TaskItem?> UpdateTaskAsync(TaskItem task);
    Task<bool> DeleteTaskAsync(int id);
    Task<IEnumerable<TaskItem>> GetTasksByStatusAsync (TaskStatus status);
    Task<IEnumerable<TaskItem>> GetOverdueTasksAsync();
}