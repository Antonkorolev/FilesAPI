namespace BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileInfo;

public interface IUpdateFileInfoTask
{
    Task UpdateInfoAsync(int fileId, string fileName,  string userCode, CancellationToken cancellationToken);
}