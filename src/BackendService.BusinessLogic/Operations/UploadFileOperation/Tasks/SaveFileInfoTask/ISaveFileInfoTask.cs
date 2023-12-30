namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;

public interface ISaveFileInfoTask
{
    Task SaveInfoAsync(string fileCode, string userCode);
}