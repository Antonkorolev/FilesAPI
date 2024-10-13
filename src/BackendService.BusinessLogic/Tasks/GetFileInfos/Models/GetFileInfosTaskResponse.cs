using FileInfo = BackendService.BusinessLogic.Tasks.GetFileInfos.Models.FileInfo;

namespace BackendService.BusinessLogic.Tasks.GetFileInfos.Models;

public sealed class GetFileInfosTaskResponse(IEnumerable<FileInfo> fileInfos)
{
    public IEnumerable<FileInfo> FileInfos { get; set; } = fileInfos;
}