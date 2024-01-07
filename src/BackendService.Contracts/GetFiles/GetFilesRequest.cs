namespace BackendService.Contracts.GetFiles;

public sealed class GetFilesRequest
{
    public IEnumerable<string> FileCodes { get; set; } = default!;
}