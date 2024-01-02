using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = DatabaseContext.FileDb.Models.File;

namespace DatabaseContext.FileDb.Configuration;

public sealed class FileInfoConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("FileInfo", "dbo");
        builder.HasKey(f => f.FileId);
    }
}