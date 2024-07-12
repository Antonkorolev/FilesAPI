namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.DeleteFilesFromTemporaryStorage;

public interface IDeleteFilesFromTemporaryStorageTask
{
    void DeleteAsync(string path);
}