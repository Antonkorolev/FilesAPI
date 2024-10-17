namespace BackendService.Contracts.UpdateFiles;

public sealed class UpdateFilesRequest
{
    public UpdateFilesRequest(IEnumerable<string> fileCodes)
    {
        FileCodes = fileCodes;
    }

    public IEnumerable<string> FileCodes { get; set; }
}