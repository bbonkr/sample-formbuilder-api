using FormBuilder.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Form> Forms { get; set; }

    public DbSet<FormItem> FormItems { get; set; }

    public DbSet<FormItemOption> FormItemOptions { get; set; }

    public DbSet<Result> Results { get; set; }

    public DbSet<FileMedia> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
