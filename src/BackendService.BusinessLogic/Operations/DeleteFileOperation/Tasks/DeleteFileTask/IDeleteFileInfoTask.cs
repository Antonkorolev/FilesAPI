namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileTask;

public interface IDeleteFileInfoTask
{
    Task DeleteFileAsync(int fileId, CancellationToken cancellationToken);
}