using System.ComponentModel.DataAnnotations;

namespace DatabaseContext.FileDb.Models;

public sealed class FileChangeHistory
{
    public int FileChangeHistoryId { get; set; }

    public int FileId { get; set; }

    public DateTime? Created { get; set; }

    [MaxLength(20)] public string? CreatedBy { get; set; }

    public DateTime? Modified { get; set; }

    [MaxLength(20)] public string? ModifiedBy { get; set; }
}