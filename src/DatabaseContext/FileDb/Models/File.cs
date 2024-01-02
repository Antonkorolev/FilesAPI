namespace DatabaseContext.FileDb.Models;

public sealed class File
{
    public int FileId { get; set; }

    public Guid FileCode { get; set; } = default!;
}