using Microsoft.EntityFrameworkCore;
using MedicalTaskFlow.Core.Models;

namespace MedicalTaskFlow.Data;
/// <summary>
/// Entity Framework Core database context.
/// Configures SQLite database and entity mapping.
/// </summary>
public class AppDbContext: DbContext
{
public DbSet<TaskItem> Tasks {get; set;} = null!;
public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
{
}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Configure TaskItem Entity
        modelBuilder.Entity<TaskItem>(entity=>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Title)
            .IsRequired()
            .HasMaxLength(200);
            entity.Property(e => e.Description)
            .HasMaxLength(1000);
            entity.Property(e => e.Priority)
            .HasConversion<int>(); // store enum as int
            entity.Property(e=>e.Status)
            .HasConversion<int>();

            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.DueDate);
        });
        
    }
}
