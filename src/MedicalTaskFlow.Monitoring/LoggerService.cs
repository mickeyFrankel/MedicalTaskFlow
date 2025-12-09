using Serilog;
using Serilog.Core;

namespace MedicalTaskFlow.Monitoring;

///<summary>
/// Centralized logging service using Serilog.
/// Logs to both files and SQLite for production monitoring simulation.
/// </summary>
public class LoggerService : IDisposable
{
    private readonly Logger _logger;

    public LoggerService()
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/app-.log",
                          rollingInterval: RollingInterval.Day,
                          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
            .WriteTo.SQLite(sqliteDbPath: "logs/monitoring.db",
                            tableName: "Logs")
            .CreateLogger();
    }

    public void LogInformation(string message, string? source = null)
    {
        _logger.Information("[{Source}] {Message}", source ?? "App", message);
    }

    public void LogWarning(string message, string? source = null)
    {
        _logger.Warning("[{Source}] {Message}", source ?? "App", message);
    }

    public void LogError(Exception ex, string message, string? source = null)
    {
        _logger.Error(ex, "[{Soruce}] {Message}", source ?? "App", message);
    }

    public void Dispose()
    {
        _logger.Dispose();
    }
}