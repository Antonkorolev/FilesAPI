using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.FileDb;

public class FileDbContext : DbContext, IFileDbContext
{
    public DbSet<FileData> FileData { get; set; } = default!;

    public FileDbContext(DbContextOptions options) : base(options)
    {
    }

    protected void OnModelCreation(ModelBuilder modelEntity)
    {
        modelEntity.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}