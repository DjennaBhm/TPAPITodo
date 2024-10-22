
using Microsoft.EntityFrameworkCore;
public class Api_TodoContext : DbContext
{
  public DbSet<Api_Todo> Api_Todo { get; set; } = null!;

  public string DbPath { get; private set; }


  public Api_TodoContext()
  {
    // Path to SQLite database file
    DbPath = "Api_Todo.db";
  }


  // The following configures EF to create a SQLite database file locally
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    // Use SQLite as database
    options.UseSqlite($"Data Source={DbPath}");
    // Optional: log SQL queries to console
    //options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
  }
}

