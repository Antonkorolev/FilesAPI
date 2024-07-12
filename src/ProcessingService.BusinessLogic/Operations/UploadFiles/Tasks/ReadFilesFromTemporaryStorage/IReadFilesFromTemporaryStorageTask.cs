namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.ReadFilesFromTemporaryStorage;

public interface IReadFilesFromTemporaryStorageTask
{
    Task<Stream> RadFileAsync(string path);
}