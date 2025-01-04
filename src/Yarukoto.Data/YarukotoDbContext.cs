using Microsoft.EntityFrameworkCore;
using Yarukoto.Data.Model;

namespace Yarukoto.Data;

public class YarukotoDbContext : DbContext
{
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Note> Notes { get; set; }

    // Read connection string from environment, fallback to default if not found
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DbConnectionString") 
                               ?? "Server=localhost;Database=yarukoto;Username=yarukoto;Password=yarukoto";
        optionsBuilder.UseNpgsql(connectionString);
    }
}