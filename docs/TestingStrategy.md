# Testing Strategy

## Overview

MedicalTaskFlow employs a comprehensive testing strategy with three layers:

1. **Unit Tests** - Test business logic in isolation
2. **Integration Tests** - Test data access with real database
3. **UI Tests** - (Future) Test user interactions

## Test Coverage Goals

- **Unit Tests**: 80%+ coverage of Core project
- **Integration Tests**: 100% coverage of Repository operations
- **Overall**: 75%+ total coverage

## Unit Testing Approach

### Framework: xUnit
- Industry standard for .NET
- Excellent VS/Rider integration
- Parallel test execution

### Mocking: Moq
```csharp
var mockRepository = new Mock<ITaskRepository>();
mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);
```

### Assertions: FluentAssertions
```csharp
result.Should().NotBeNull();
result.Title.Should().Be("Expected Title");
await act.Should().ThrowAsync<ArgumentNullException>();
```

## Integration Testing Approach

### In-Memory SQLite
```csharp
var options = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite("DataSource=:memory:")
    .Options;
```

**Benefits:**
- Fast execution
- Test isolation (fresh database per test)
- No cleanup required

### Test Structure
```csharp
[Fact]
public async Task AddAsync_ShouldPersistTaskToDatabase()
{
    // Arrange
    using var context = _fixture.CreateContext();
    var repository = new TaskRepository(context);
    
    // Act
    var result = await repository.AddAsync(task);
    
    // Assert
    result.Id.Should().BeGreaterThan(0);
}
```

## Test Organization
```
tests/
├── MedicalTaskFlow.Tests.Unit/
│   ├── Models/
│   │   └── TaskItemTests.cs          # Test domain models
│   └── Services/
│       └── TaskServiceTests.cs        # Test business logic
│
└── MedicalTaskFlow.Tests.Integration/
    ├── TestDatabaseFixture.cs         # Test setup
    └── Repositories/
        └── TaskRepositoryTests.cs     # Test data access
```

## Running Tests

### All Tests
```bash
dotnet test
```

### Unit Tests Only
```bash
dotnet test tests/MedicalTaskFlow.Tests.Unit/
```

### With Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Detailed Output
```bash
dotnet test --logger "console;verbosity=detailed"
```

## Test Naming Convention
```csharp
[Method]_[Scenario]_[ExpectedBehavior]

// Examples:
CreateTaskAsync_WithValidTask_ShouldSetCreatedAt
CreateTaskAsync_WithNullTask_ShouldThrowArgumentNullException
GetOverdueTasksAsync_ShouldReturnOnlyOverdueTasks
```

## Continuous Integration

Tests run automatically on every push via GitHub Actions:
```yaml
- name: Run tests
  run: dotnet test --no-build --verbosity normal
```

## What We Test

### Unit Tests Cover:
- ✅ Input validation
- ✅ Business rule enforcement
- ✅ Exception handling
- ✅ Domain model behavior
- ✅ Service logic

### Integration Tests Cover:
- ✅ Database CRUD operations
- ✅ Entity Framework mappings
- ✅ Transaction handling
- ✅ Data persistence
- ✅ Query correctness

### Not Tested (Yet):
- ❌ UI interactions (would use FlaUI)
- ❌ Logging functionality
- ❌ Performance benchmarks

## Medical Device Software Considerations

In medical device software, testing is critical for:

1. **Safety**: Ensure correct behavior under all conditions
2. **Regulatory Compliance**: FDA 510(k) requires test documentation
3. **Traceability**: Tests link requirements to implementation
4. **Regression Prevention**: Catch breaking changes early

This project demonstrates testing rigor expected in medical software environments.