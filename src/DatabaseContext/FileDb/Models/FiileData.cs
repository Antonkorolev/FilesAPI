using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.FileDb.Models;

public sealed class FileData
{
    public int FileDataId { get; set; }

    [MaxLength(20)]
    public string FileCode { get; set; } = default!;

    public byte[] File { get; set; } = default!;
}