using Microsoft.EntityFrameworkCore;
using pv178_project.Models;

namespace pv178_project.Db;

public class ScoreContext : DbContext
{
    public DbSet<DbScoreResult> ScoreResults { get; set; }
    public string DbPath { get; }

    public ScoreContext()
    {
        var folder = AppDomain.CurrentDomain.BaseDirectory;
        DbPath = Path.Join(folder, "..", "..", "..", "Db", "score.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbScoreResult>();
    }
}