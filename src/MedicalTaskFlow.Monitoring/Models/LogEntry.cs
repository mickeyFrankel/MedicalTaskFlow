namespace MedicalTaskFlow.Monitoring.Models;

/// <summary>
/// Represents a structured log entry for monitoring dashboard.
/// Mimics production logging systems like kibana.
/// </summary>
public class LogEntry
{
    public int Id {get; set;}
    public DateTime Timestamp {get; set;}
    public string Level {get; set;} = String.Empty; // Info, Warning, Error
    public string Message {get; set;} = String.Empty;
    public string? Exception {get; set;}
    public string? Source { get; set; }
}