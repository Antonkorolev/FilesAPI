using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.FileDb.Models;

public sealed class FileInfo
{
    public int FileInfoId { get; set; }

    [MaxLength(100)] public string Code { get; set; } = default!;

    [MaxLength(50)] public string Name { get; set; } = default!;
}