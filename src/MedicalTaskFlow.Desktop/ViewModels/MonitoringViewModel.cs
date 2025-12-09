using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using MedicalTaskFlow.Monitoring.Models;
using Serilog;

namespace MedicalTaskFlow.Desktop.ViewModels;

///<summary>
/// ViewModel for production monitoring dashboard.
/// Simulates Kibana/DataBricks log viewing capabilites.
///</summary>
public partial class MonitoringViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<LogEntry> _logs = new();
    
    [ObservableProperty]
    private string _filterLevel = "All";

    [ObservableProperty]
    private int _errorCount;

    [ObservableProperty]
    private int _warningCount;

    public MonitoringViewModel()
    {
        LoadLogsAsync();
    }

    [RelayCommand]
    private async Task LoadLogsAsync()
    {
        // Simulate loading logs from a data source
        await Task.Run(() =>
        {
            var logs = new List<LogEntry>();
            using var connection = new SqliteConnection("Data Source=logs/monitoring.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT Id, Timestamp, Level, Message, Exception
            FROM Logs
            ORDER BY Timestamp DESC
            LIMIT 100";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                logs.Add(new LogEntry
                {
                    Id = reader.GetInt32(0),
                    Timestamp = reader.GetDateTime(1),
                    Level = reader.GetString(2),
                    Message = reader.GetString(3),
                    Exception = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            Logs = new ObservableCollection<LogEntry>(logs);
            ErrorCount = logs.Count(l => l.Level == "Error");
            WarningCount = logs.Count(l => l.Level == "Warning");
        });
    }

    [RelayCommand]
    private async Task ClearLogAsync()
    {
        await Task.Run(() =>
        {
            using var connection = new SqliteConnection("Data Source=logs/monitoring.db");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Logs";
            command.ExecuteNonQuery();
        });
        await LoadLogsAsync();
    }
}   