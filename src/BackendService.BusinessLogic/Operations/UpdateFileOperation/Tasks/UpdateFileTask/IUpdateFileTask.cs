namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileTask;

public interface IUpdateFileTask
{
    Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken);
}