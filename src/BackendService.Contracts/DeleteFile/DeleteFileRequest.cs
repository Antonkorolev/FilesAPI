namespace BackendService.Contracts.DeleteFile;

public sealed class DeleteFileRequest
{
    public string FileCode { get; set; } = default!;
}