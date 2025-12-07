using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MedicalTaskFlow.Core.Interfaces;
using MedicalTaskFlow.Core.Services;
using MedicalTaskFlow.Data;
using MedicalTaskFlow.Data.Repositories;
using MedicalTaskFlow.Desktop.ViewModels;

namespace MedicalTaskFlow.Desktop;

/// <summary>
/// Application entry point with dependecny injection configuration.
/// </summary>
public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        //Configure services
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();

        // Initialize database
        InitialzeDatabse();

        // Show main window
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Register DbContext with SQLite provider
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=medicaltaskflow.db"));

        // Register repositories
        services.AddScoped<ITaskRepository, TaskRepository>();

        // Register services
        services.AddScoped<ITaskService, TaskService>();

        // Register ViewModels
        services.AddTransient<MainViewModel>();

        // Register Views
        services.AddTransient<MainWindow>();
    }

    private void InitialzeDatabse()
    {
        using var scope = _serviceProvider!.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate(); // Apply migrations at startup
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
