using DatabaseContext.FileDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseContext.FileDb.Configuration;

public sealed class FileDataConfiguration : IEntityTypeConfiguration<FileData>
{
    public void Configure(EntityTypeBuilder<FileData> builder)
    {
        builder.ToTable("FileData", "dbo");
        builder.HasKey(f => f.FileDataId);
    }
}