namespace BackendService.BusinessLogic.Helpers.Models.Response;

public sealed class PathBuilderResponse
{
    public PathBuilderResponse(IEnumerable<PathBuilderFileData> fileData)
    {
        FileData = fileData;
    }

    public IEnumerable<PathBuilderFileData> FileData { get; set; }
}