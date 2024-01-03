namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileFromDbTask;

public sealed class DeleteFileFromDbTask : IDeleteFileFromDbTask
{
    public void Delete(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException();
        File.Delete(path);
    }
}