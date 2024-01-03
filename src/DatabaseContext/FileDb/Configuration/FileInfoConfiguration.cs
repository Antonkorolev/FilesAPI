using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FileInfo = DatabaseContext.FileDb.Models.FileInfo;

namespace DatabaseContext.FileDb.Configuration;

public sealed class FileInfoConfiguration : IEntityTypeConfiguration<FileInfo>
{
    public void Configure(EntityTypeBuilder<FileInfo> builder)
    {
        builder.ToTable("FileInfo", "dbo");
        builder.HasKey(f => f.FileInfoId);
    }
}