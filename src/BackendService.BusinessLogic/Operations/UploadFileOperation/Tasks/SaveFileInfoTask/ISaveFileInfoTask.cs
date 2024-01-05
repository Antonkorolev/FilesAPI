namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;

public interface ISaveFileInfoTask
{
    Task SaveAsync(string fileCode, string userCode, string fileName, CancellationToken cancellationToken);
}