namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public interface IGetFileTask
{
    Stream Get(string path);
}