namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileInfoTask;

public interface IUpdateFileInfoTask
{
    Task UpdateInfoAsync(Guid fileCode, string userCode, CancellationToken cancellationToken);
}