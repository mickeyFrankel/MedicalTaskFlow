# MedicalTaskFlow - Architecture Documentation

## System Overview

MedicalTaskFlow is an enterprise-grade desktop task management application built with C#/.NET 7, WPF, and SQLite. The architecture follows clean architecture principles with clear separation of concerns across layers.

## Architecture Diagram
```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  MainWindow.xaml  │  MonitoringView.xaml             │  │
│  │  MainViewModel    │  MonitoringViewModel             │  │
│  └──────────────────────────────────────────────────────┘  │
│         (WPF + MVVM + Data Binding + Commands)              │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│                    Business Logic Layer                      │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  TaskService  (ITaskService)                         │  │
│  │  - CreateTaskAsync                                    │  │
│  │  - UpdateTaskAsync                                    │  │
│  │  - DeleteTaskAsync                                    │  │
│  │  - GetOverdueTasksAsync                              │  │
│  └──────────────────────────────────────────────────────┘  │
│         (Business Rules + Validation + Logging)              │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│                     Data Access Layer                        │
│  ┌──────────────────────────────────────────────────────┐  │
│  │  TaskRepository (ITaskRepository)                    │  │
│  │  AppDbContext (Entity Framework Core)                │  │
│  └──────────────────────────────────────────────────────┘  │
│         (Repository Pattern + EF Core + Migrations)          │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│                      SQLite Database                         │
│                   medicaltaskflow.db                         │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                  Cross-Cutting Concerns                      │
│  - Serilog (File + SQLite logging)                          │
│  - Dependency Injection (Microsoft.Extensions.DI)           │
│  - CommunityToolkit.Mvvm                                    │
└─────────────────────────────────────────────────────────────┘
```

## Design Patterns

### MVVM (Model-View-ViewModel)
- **Model**: `TaskItem`, domain entities
- **View**: XAML files (MainWindow.xaml, MonitoringView.xaml)
- **ViewModel**: MainViewModel, MonitoringViewModel
- **Benefits**: Testability, separation of UI and logic, data binding

### Repository Pattern
- Interface: `ITaskRepository`
- Implementation: `TaskRepository`
- **Benefits**: Data access abstraction, easier testing, swappable persistence

### Dependency Injection
- Configured in `App.xaml.cs`
- Service lifetimes: Singleton (LoggerService), Scoped (DbContext), Transient (ViewModels)
- **Benefits**: Loose coupling, testability, maintainability

### Command Pattern
- `RelayCommand` from CommunityToolkit.Mvvm
- All user actions are commands (AddTaskCommand, DeleteTaskCommand, etc.)
- **Benefits**: Undo/redo capability, separation of action from UI

## Project Structure
```
MedicalTaskFlow/
├── src/
│   ├── MedicalTaskFlow.Core/           # Business logic
│   │   ├── Models/                     # Domain entities
│   │   ├── Interfaces/                 # Service contracts
│   │   └── Services/                   # Business logic implementation
│   │
│   ├── MedicalTaskFlow.Data/           # Data access
│   │   ├── AppDbContext.cs            # EF Core context
│   │   ├── Repositories/              # Repository implementations
│   │   └── Migrations/                # Database migrations
│   │
│   ├── MedicalTaskFlow.Desktop/        # WPF UI
│   │   ├── Views/                     # XAML views
│   │   ├── ViewModels/                # ViewModels
│   │   └── App.xaml.cs                # DI configuration
│   │
│   └── MedicalTaskFlow.Monitoring/     # Logging & monitoring
│       ├── LoggerService.cs
│       └── Models/
│
└── tests/
    ├── MedicalTaskFlow.Tests.Unit/     # Unit tests (Moq + FluentAssertions)
    └── MedicalTaskFlow.Tests.Integration/  # Integration tests (in-memory SQLite)
```

## Technology Stack

| Layer | Technologies |
|-------|-------------|
| UI | WPF, XAML, CommunityToolkit.Mvvm |
| Business Logic | C# 11, .NET 7 |
| Data Access | Entity Framework Core 7, SQLite |
| Logging | Serilog (File + SQLite sinks) |
| Testing | xUnit, Moq, FluentAssertions |
| Dependency Injection | Microsoft.Extensions.DependencyInjection |

## Key Design Decisions

### Why SQLite?
- Lightweight, no server required
- Perfect for desktop applications
- Easy deployment (single .db file)
- Supports migrations via EF Core

### Why MVVM?
- Industry standard for WPF applications
- Testable ViewModels (no UI dependencies)
- Data binding reduces boilerplate code
- Aligns with medical device software best practices

### Why Repository Pattern?
- Abstracts data access from business logic
- Enables easy switching between databases
- Simplifies testing (mock repositories)
- Follows enterprise architecture patterns

## Medical Device Software Alignment

This architecture mirrors patterns used in medical device software:

1. **Layered Architecture**: Clear separation between UI, business logic, and data
2. **Testability**: >75% code coverage across unit and integration tests
3. **Logging**: Comprehensive logging for production monitoring (like Kibana)
4. **Validation**: Input validation at service layer
5. **Error Handling**: Structured exception handling with logging

## Future Enhancements

- **Authentication**: User login system
- **Audit Trail**: Track all data changes (regulatory compliance)
- **Export**: Export tasks to PDF/Excel
- **Notifications**: Due date reminders
- **Cloud Sync**: Azure SQL Database integration