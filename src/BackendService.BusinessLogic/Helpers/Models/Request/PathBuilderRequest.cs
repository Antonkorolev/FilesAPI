namespace BackendService.BusinessLogic.Helpers.Models.Request;

public sealed class PathBuilderRequest
{
    public PathBuilderRequest(IEnumerable<PathBuilderFileInfo> fileInfos)
    {
        FileInfos = fileInfos;
    }

    public IEnumerable<PathBuilderFileInfo> FileInfos { get; set; }
}