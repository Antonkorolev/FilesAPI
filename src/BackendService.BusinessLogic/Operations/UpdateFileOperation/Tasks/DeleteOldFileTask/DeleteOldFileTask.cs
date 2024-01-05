namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.DeleteOldFileTask;

public sealed class DeleteOldFileTask : IDeleteOldFileTask
{
    public void Delete(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException();
        File.Delete(path);
    }
}