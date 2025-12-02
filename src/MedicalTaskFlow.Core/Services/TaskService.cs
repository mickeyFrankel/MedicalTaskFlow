using MedicalTaskFlow.Core.Interfaces;
using MedicalTaskFlow.Core.Models;

namespace MedicalTaskFlow.Core.Services;
///<summary>
/// Business logic omplementation for task operations.
/// Depends on ITaskRepository abstraction
/// </summary>
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    public TaskService(ITaskRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
    {
        return await _repository.GetAllAsync();
    }
        public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        if(id<=0)
            throw new ArgumentException("Task ID must be positive", nameof(id));
        return await _repository.GetByIdAsync(id);
    }
        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Task title cannot be empty", nameof(task.Title));
        task.CreatedAt = DateTime.UtcNow;
        return await _repository.AddAsync(task);
    }
      public async Task<TaskItem?> UpdateTaskAsync(TaskItem task)
    {
        ArgumentNullException.ThrowIfNull(task);
        if (task.Id <=0)
            throw new ArgumentException("Invalid task ID", nameof(task));
        return await _repository.UpdateAsync(task);
    }
        public async Task<bool> DeleteTaskAsync(int id)
    {
        if(id<=0)
            throw new ArgumentException("task ID must be positive", nameof(id));
        return await _repository.DeleteAsync(id);
    }
        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TaskItem.TaskStatus status)
    {
        var allTasks = await _repository.GetAllAsync();
        return allTasks.Where(t => t.Status == status).ToList();
    }
        public async Task<IEnumerable<TaskItem>> GetOverdueTasksAsync()
    {
        var allTasks = await _repository.GetAllAsync();
        return allTasks.Where(t =>t.IsOverdue()).ToList();
    }
}





  





