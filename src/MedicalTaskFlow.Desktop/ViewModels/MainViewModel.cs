using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MedicalTaskFlow.Core.Interfaces;
using MedicalTaskFlow.Core.Models;

namespace MedicalTaskFlow.Desktop.ViewModels;
///<summary>
/// ViewModel for the main application window.
/// Manages task list and user interactions.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly ITaskService _taskService;

    [ObservableProperty]
    private ObservableCollection<TaskItem> _tasks = new ObservableCollection<TaskItem>();

    [ObservableProperty]
    private TaskItem? _selectedTask;

    [ObservableProperty]
    private string _newTaskTitle = String.Empty;

    [ObservableProperty]
    private string _statusMessage = "Ready";
    public MainViewModel(ITaskService taskService)
    {
        _taskService = taskService;
        LoadTasksAsync();
    }

    [RelayCommand]
    private async Task LoadTasksAsync()
    {
        try
        {
            StatusMessage = "Loading tasks...";
            var tasks = await _taskService.GetAllTasksAsync();
            Tasks = new ObservableCollection<TaskItem>(tasks);
            StatusMessage = $"Loaded {Tasks.Count} tasks";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading tasks: {ex.Message}";
            MessageBox.Show($"Failed to load tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTaskTitle))
        {
            MessageBox.Show("Task title cannot be empty.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        try
        {
            var newTask = new TaskItem
            {
                Title = NewTaskTitle,
                Priority = TaskItem.TaskPriority.Medium,
                Status = TaskItem.TaskStatus.Todo
            };
            await _taskService.CreateTaskAsync(newTask);
            NewTaskTitle = string.Empty;
            await LoadTasksAsync();
            StatusMessage = "Task created successfully.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error creating task: {ex.Message}";
            MessageBox.Show($"Failed to update task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task DeleteTaskAsync()
    {
        if (SelectedTask == null)
        {
            MessageBox.Show("Please select a task to delete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        var result = MessageBox.Show($"Delete task '{SelectedTask.Title}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                await _taskService.DeleteTaskAsync(SelectedTask.Id);
                await LoadTasksAsync();
                StatusMessage = "Task deleted successfully.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error deleting task: {ex.Message}";
                MessageBox.Show($"Failed to delete task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
    [RelayCommand]
    private async Task CompleteTaskAsync()
    {
        if (SelectedTask == null)
        {
            MessageBox.Show("Please select a task to complete.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }
        try
        {
            SelectedTask.MarkAsComplete();
            await _taskService.UpdateTaskAsync(_selectedTask);
            await LoadTasksAsync();
            StatusMessage = "Task marked as complete";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error completing task: {ex.Message}";
            MessageBox.Show($"Failed to complete task: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    }


}