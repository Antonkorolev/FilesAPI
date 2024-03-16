namespace BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFile;

public interface IGetFileTask
{
    Task<Stream> GetAsync(string path);
}