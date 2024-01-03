namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.WriteFileTask;

public interface IWriteFileTask
{
    Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken);
}