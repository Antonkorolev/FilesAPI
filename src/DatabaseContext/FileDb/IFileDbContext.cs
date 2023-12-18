using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseContext.FileDb;

public interface IFileDbContext : IDataContext
{
    DbSet<FileData> FileData { get; set; }
}