namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Tasks.DeleteFileTask;

public sealed class DeleteFileTask : IDeleteFileTask
{
    public void Delete(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException();
        File.Delete(path);
    }
}