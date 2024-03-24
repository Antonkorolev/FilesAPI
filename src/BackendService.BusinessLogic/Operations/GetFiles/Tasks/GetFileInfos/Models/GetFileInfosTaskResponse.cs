namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos.Models;

public sealed class GetFileInfosTaskResponse
{
    public GetFileInfosTaskResponse(IEnumerable<FileInfo> fileInfos)
    {
        FileInfos = fileInfos;
    }

    public IEnumerable<FileInfo> FileInfos { get; set; }
}