namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public interface IGetFileTask
{
    Task<Stream> GetAsync(string path);
}