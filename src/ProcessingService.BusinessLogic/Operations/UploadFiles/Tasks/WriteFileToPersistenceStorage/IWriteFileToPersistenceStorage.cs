namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFileToPersistenceStorage;

public interface IWriteFileToPersistenceStorage
{
    Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken);
}