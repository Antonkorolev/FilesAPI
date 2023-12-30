using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.FileDb.Models;

public sealed class FileData
{
    public int FileId { get; set; }

    [MaxLength(20)] 
    public string FileCode { get; set; } = default!;

    public DateTime Created { get; set; }

    [MaxLength(20)] 
    public string CreatedBy { get; set; } = default!;

    public DateTime? Modified { get; set; }

    [MaxLength(20)] 
    public string? ModifiedBy { get; set; } = default!;
}