using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using File = DatabaseContext.FileDb.Models.File;

namespace DatabaseContext.FileDb;

public interface IFileDbContext : IDataContext
{
    DbSet<File> File { get; set; }

    DbSet<FileChangeHistory> FileChangeHistory { get; set; }
}