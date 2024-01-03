using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace DatabaseContext.FileDb;

public class FileDbContext : DbContext, IFileDbContext
{
    public DbSet<FileInfo> FileInfo { get; set; } = default!;

    public DbSet<FileChangeHistory> FileChangeHistory { get; set; } = default!;

    public FileDbContext(DbContextOptions options) : base(options)
    {
    }

    protected void OnModelCreation(ModelBuilder modelEntity)
    {
        modelEntity.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}