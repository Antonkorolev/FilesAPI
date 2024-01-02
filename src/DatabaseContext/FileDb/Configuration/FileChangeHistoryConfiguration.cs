using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.FileDb.Configuration;

public sealed class FileChangeHistoryConfiguration : IEntityTypeConfiguration<FileChangeHistory>
{
    public void Configure(EntityTypeBuilder<FileChangeHistory> builder)
    {
        builder.ToTable("FileChangeHistory", "dbo");
        builder.HasKey(f => f.FileChangeHistoryId);
    }
}