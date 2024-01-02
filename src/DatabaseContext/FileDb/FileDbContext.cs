using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using File = DatabaseContext.FileDb.Models.File;

namespace DatabaseContext.FileDb;

public class FileDbContext : DbContext, IFileDbContext
{
    public DbSet<File> File { get; set; } = default!;

    public DbSet<FileChangeHistory> FileChangeHistory { get; set; } = default!;

    public FileDbContext(DbContextOptions options) : base(options)
    {
    }

    protected void OnModelCreation(ModelBuilder modelEntity)
    {
        modelEntity.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}