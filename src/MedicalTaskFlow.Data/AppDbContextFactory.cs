using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MedicalTaskFlow.Data;
/// <summary>
/// Factory for creating AppDbContext at design time.
/// Used by EF Core CLI tools (dotnet ef migrations, etc.) to instantiate the DbContext
/// without running the full application. Runtime uses dependency injection instead.
/// </summary>

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        // Use SQLite for development
        optionsBuilder.UseSqlite("Data Source=medicaltaskflow.db");
        
        return new AppDbContext(optionsBuilder.Options);
    }
}