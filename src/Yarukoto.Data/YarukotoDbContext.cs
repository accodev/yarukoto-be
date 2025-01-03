using Microsoft.EntityFrameworkCore;
using Yarukoto.Data.Model;

namespace Yarukoto.Data;

public class YarukotoDbContext : DbContext
{
    public DbSet<Workspace> Workspaces { get; set; }
    public DbSet<Note> Notes { get; set; }

    // todo: read connection string from environment, fallback to default if not found
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;Database=yarukoto;Username=yarukoto;Password=yarukoto");
}