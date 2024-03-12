namespace BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFileTask;

public interface IGetFileTask
{
    Task<Stream> GetAsync(string path);
}