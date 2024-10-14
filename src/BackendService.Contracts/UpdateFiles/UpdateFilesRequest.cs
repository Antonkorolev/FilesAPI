namespace BackendService.Contracts.UpdateFiles;

public sealed class UpdateFilesRequest
{
    public IEnumerable<UpdateFile> UpdateFiles = default!;
}