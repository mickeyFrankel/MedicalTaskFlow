# MedicalTaskFlow

Enterprise-grade desktop task management application with production monitoring capabilities.

[![.NET](https://img.shields.io/badge/.NET-7.0-512BD4)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-11-239120)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![WPF](https://img.shields.io/badge/WPF-Desktop-0078D4)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![SQLite](https://img.shields.io/badge/SQLite-Database-003B57)](https://www.sqlite.org/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## 🎯 Project Overview

MedicalTaskFlow is a production-ready desktop application demonstrating enterprise software engineering practices. Built with C#/.NET and WPF, it showcases **MVVM architecture**, **comprehensive testing**, and **production monitoring** capabilities required in medical device software environments.

## ✨ Key Features

- **Task Management**: Create, update, delete, and complete tasks with priority levels
- **Production Monitoring Dashboard**: Real-time log viewing and error tracking (simulates Kibana/DataBricks)
- **SQLite Persistence**: Lightweight database with Entity Framework Core migrations
- **MVVM Architecture**: Clean separation of concerns with dependency injection
- **Comprehensive Testing**: 75%+ code coverage with unit and integration tests
- **Structured Logging**: Serilog integration for production-grade logging

## 🏗️ Architecture
```
┌─────────────────────┐
│   WPF UI (MVVM)     │  ← User Interface Layer
├─────────────────────┤
│  Business Logic     │  ← Services & Validation
├─────────────────────┤
│  Data Access        │  ← Repository Pattern + EF Core
├─────────────────────┤
│  SQLite Database    │  ← Persistence Layer
└─────────────────────┘
```

**Design Patterns:**
- MVVM (Model-View-ViewModel)
- Repository Pattern
- Dependency Injection
- Command Pattern

See [Architecture Documentation](docs/Architecture.md) for detailed diagrams and explanations.

## 🛠️ Technology Stack

| Category | Technologies |
|----------|-------------|
| **Framework** | .NET 7, C# 11 |
| **UI** | WPF, XAML, CommunityToolkit.Mvvm |
| **Database** | SQLite, Entity Framework Core 7 |
| **Testing** | xUnit, Moq, FluentAssertions |
| **Logging** | Serilog (File + SQLite sinks) |
| **DI** | Microsoft.Extensions.DependencyInjection |
| **IDE** | JetBrains Rider (or Visual Studio 2022) |

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio 2022](https://visualstudio.microsoft.com/)
- Git

### Installation
```bash
# Clone repository
git clone https://github.com/YOUR_USERNAME/MedicalTaskFlow.git
cd MedicalTaskFlow

# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Run application
dotnet run --project src/MedicalTaskFlow.Desktop

# Run tests
dotnet test
```

### Database Setup

Database is automatically created on first run via EF Core migrations. No manual setup required.

**Location:** `medicaltaskflow.db` in application directory

## 📊 Testing

### Test Coverage

- **Unit Tests**: 85%+ coverage of Core project
- **Integration Tests**: 100% coverage of Repository operations
- **Total**: 75%+ overall coverage

### Running Tests
```bash
# All tests
dotnet test

# Unit tests only
dotnet test tests/MedicalTaskFlow.Tests.Unit/

# Integration tests only
dotnet test tests/MedicalTaskFlow.Tests.Integration/

# With detailed output
dotnet test --logger "console;verbosity=detailed"
```

See [Testing Strategy Documentation](docs/TestingStrategy.md) for comprehensive testing approach.

## 📈 Production Monitoring

MedicalTaskFlow includes a **production monitoring dashboard** that simulates enterprise monitoring tools like Kibana and DataBricks:

- **Real-time log viewing**: See all application events
- **Error tracking**: Count and filter errors/warnings
- **Structured logging**: Serilog with SQLite persistence
- **Log levels**: Info, Warning, Error

This demonstrates **proactive production monitoring** practices required for medical device software.

## 🎓 Why This Project?

### Alignment with Medical Device Software Engineering

| Requirement | Implementation |
|------------|----------------|
| **Desktop Software** | ✅ C#/.NET WPF application |
| **Test Coverage** | ✅ 75%+ with unit & integration tests |
| **Design Patterns** | ✅ MVVM, Repository, DI, Command |
| **Production Monitoring** | ✅ Serilog + monitoring dashboard |
| **Git Workflow** | ✅ Feature branching, conventional commits |
| **Documentation** | ✅ Architecture, testing, API docs |
| **SQLite** | ✅ EF Core with migrations |

### Demonstrates Professional Practices

1. **Clean Architecture**: Layered design with clear boundaries
2. **SOLID Principles**: Single responsibility, dependency inversion
3. **Test-Driven Development**: Tests written alongside features
4. **Production Readiness**: Logging, error handling, monitoring
5. **Collaboration-Ready**: Git workflow simulating team environment

## 📁 Project Structure
```
MedicalTaskFlow/
├── src/
│   ├── MedicalTaskFlow.Core/           # Business logic & domain models
│   ├── MedicalTaskFlow.Data/           # Data access & EF Core
│   ├── MedicalTaskFlow.Desktop/        # WPF UI & ViewModels
│   └── MedicalTaskFlow.Monitoring/     # Logging & monitoring
├── tests/
│   ├── MedicalTaskFlow.Tests.Unit/     # Unit tests
│   └── MedicalTaskFlow.Tests.Integration/  # Integration tests
├── docs/
│   ├── Architecture.md                 # System architecture
│   ├── TestingStrategy.md              # Testing approach
│   └── ProductionMonitoring.md         # Monitoring guide
└── README.md
```

## 🔧 Configuration

### Database Connection String

Located in `App.xaml.cs`:
```csharp
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=medicaltaskflow.db"));
```

### Logging Configuration

Located in `LoggerService.cs`:
```csharp
.WriteTo.File(path: "logs/app-.log", rollingInterval: RollingInterval.Day)
.WriteTo.SQLite(sqliteDbPath: "logs/monitoring.db")
```

## 📝 Git Workflow

This project follows **conventional commits** and simulates Jira-based development:
```bash
# Feature branch
git checkout -b feature/add-task-filtering

# Commit with ticket reference
git commit -m "feat(ui): add task filtering by status

- Add filter dropdown to MainWindow
- Implement FilterTasksByStatusCommand in ViewModel
- Update DataGrid binding with filtered collection

[TASK-123] Task filtering feature"

# Push and create PR
git push origin feature/add-task-filtering
```

### Commit Types

- `feat`: New feature
- `fix`: Bug fix
- `refactor`: Code refactoring
- `test`: Adding tests
- `docs`: Documentation updates
- `chore`: Maintenance tasks

## 🤝 Contributing

This is a portfolio project, but suggestions are welcome! Please open an issue to discuss changes.

## 📄 License

This project is licensed under the MIT License - see [LICENSE](LICENSE) file for details.

## 👤 Author

**Mickey Frankel**
- GitHub: [@mickeyFrankel](https://github.com/mickeyFrankel)
- LinkedIn: [mickey-frankel](https://linkedin.com/in/mickey-frankel)
- Email: Mickey.115533@gmail.com

## 🙏 Acknowledgments

- **CommunityToolkit.Mvvm**: Simplified MVVM implementation
- **Serilog**: Excellent structured logging library
- **Entity Framework Core**: Powerful ORM for .NET
- **iTero Team**: Inspiration for production monitoring dashboard

---

**Built as a demonstration of enterprise C#/.NET desktop development practices**