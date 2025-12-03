using Microsoft.EntityFrameworkCore;
using MedicalTaskFlow.Core.Models;
using MedicalTaskFlow.Core.Interfaces;

namespace MedicalTaskFlow.Data.Repositories;

///<summary>
/// SQLite implementation of task repository.
/// Handles all database operations for tasks.
/// </summary>
public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.
        OrderByDescending(t => t.CreatedAt).
        ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FindAsync(id);
    }

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateAsync(TaskItem task)
    {
    var existingTask = await _context.Tasks.FindAsync(task.Id);
    if (existingTask == null)
        return null;  // Task doesn't exist
    
    _context.Entry(existingTask).CurrentValues.SetValues(task);
    await _context.SaveChangesAsync();
    return existingTask;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}