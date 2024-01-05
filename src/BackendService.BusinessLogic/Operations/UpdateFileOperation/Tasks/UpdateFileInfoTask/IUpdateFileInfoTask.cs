namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;

public interface IUpdateFileInfoTask
{
    Task UpdateInfoAsync(int fileId, string fileCode, string fileName,  string userCode, CancellationToken cancellationToken);
}