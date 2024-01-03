using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.FileDb.Models;

public sealed class FileInfo
{
    public int FileInfoId { get; set; }

    public Guid Code { get; set; }

    [MaxLength(20)] public string Name { get; set; } = default!;
}