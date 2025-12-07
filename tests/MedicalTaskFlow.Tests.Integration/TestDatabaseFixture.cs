using Microsoft.EntityFrameworkCore;
using MedicalTaskFlow.Data;

namespace MedicalTaskFlow.Tests.Integration;

/// <summary>
/// Test fixtue for creating in-memory SQLite databases.
/// Ensures test isolation by creating freash database for each test.
/// </summary>
public class TestDatabaseFixture : IDisposable
{
    public AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new AppDbContext(options);
        context.Database.OpenConnection(); // keep connection alive for in-memory db
        context.Database.EnsureCreated();

        return context;
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}