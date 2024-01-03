using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace DatabaseContext.FileDb;

public interface IFileDbContext : IDataContext
{
    DbSet<FileInfo> FileInfo { get; set; }

    DbSet<FileChangeHistory> FileChangeHistory { get; set; }
}