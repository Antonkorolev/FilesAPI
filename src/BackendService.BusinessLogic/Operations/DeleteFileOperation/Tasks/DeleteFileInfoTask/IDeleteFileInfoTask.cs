namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileInfoTask;

public interface IDeleteFileInfoTask
{
    Task DeleteFileAsync(int fileId, CancellationToken cancellationToken);
}