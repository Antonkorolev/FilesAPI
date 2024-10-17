namespace ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFileInfo;

public interface IUpdateFileInfoTask
{
    Task UpdateInfoAsync(int fileId, string fileName,  string userCode, CancellationToken cancellationToken);
}