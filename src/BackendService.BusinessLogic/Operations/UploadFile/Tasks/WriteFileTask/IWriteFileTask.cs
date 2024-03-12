namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.WriteFileTask;

public interface IWriteFileTask
{
    Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken);
}