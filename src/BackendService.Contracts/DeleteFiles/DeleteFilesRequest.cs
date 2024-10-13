namespace BackendService.Contracts.DeleteFiles;

public sealed class DeleteFilesRequest
{
    public IEnumerable<string> FileCodes { get; set; } = default!;
}