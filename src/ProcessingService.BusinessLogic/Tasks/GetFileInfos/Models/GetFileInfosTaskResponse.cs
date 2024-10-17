namespace ProcessingService.BusinessLogic.Tasks.GetFileInfos.Models;

public sealed class GetFileInfosTaskResponse(IEnumerable<FileInfo> fileInfos)
{
    public IEnumerable<FileInfo> FileInfos { get; set; } = fileInfos;
}