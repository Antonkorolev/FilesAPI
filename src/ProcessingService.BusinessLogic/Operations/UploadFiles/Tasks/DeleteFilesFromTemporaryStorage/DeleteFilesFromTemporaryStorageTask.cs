namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.DeleteFilesFromTemporaryStorage;

public sealed class DeleteFilesFromTemporaryStorageTask : IDeleteFilesFromTemporaryStorageTask
{
    public void DeleteAsync(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException(path);
        File.Delete(path);
    }
}