namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFile;

public interface IWriteFileTask
{
    Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken);
}