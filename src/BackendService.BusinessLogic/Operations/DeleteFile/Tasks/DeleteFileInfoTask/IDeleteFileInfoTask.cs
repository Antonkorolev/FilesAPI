namespace BackendService.BusinessLogic.Operations.DeleteFile.Tasks.DeleteFileInfoTask;

public interface IDeleteFileInfoTask
{
    Task DeleteFileAsync(int fileInfoId, CancellationToken cancellationToken);
}