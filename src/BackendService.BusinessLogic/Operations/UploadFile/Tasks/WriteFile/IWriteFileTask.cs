namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.WriteFile;

public interface IWriteFileTask
{
    Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken);
}