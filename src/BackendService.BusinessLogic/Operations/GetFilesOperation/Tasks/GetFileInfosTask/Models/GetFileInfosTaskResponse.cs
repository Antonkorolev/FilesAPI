namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask.Models;

public sealed class GetFileInfosTaskResponse
{
    public GetFileInfosTaskResponse(IEnumerable<FileInfo> fileInfos)
    {
        FileInfos = fileInfos;
    }

    public IEnumerable<FileInfo> FileInfos { get; set; }
}