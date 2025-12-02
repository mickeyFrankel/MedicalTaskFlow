using ModelTaskFlow.Core.Models;
namespace MedicalTaskFlow.Core.Interfaces;
///<summary>
/// Repository interface for data access.
/// Abstracts persistence layer from business logic.
/// </summary>
public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> AddAsync(TaskItem task);
    Task<TaskItem?> UpdateAsync(TaskItem task);
    Task<bool> DeleteAsync(int id);
}