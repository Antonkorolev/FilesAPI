namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public sealed class GetFileTask : IGetFileTask
{
    public Stream Get(string path)
    {
        return File.OpenRead(path);
    }
}