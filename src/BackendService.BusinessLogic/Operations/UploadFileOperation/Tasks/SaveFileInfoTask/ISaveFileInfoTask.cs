namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;

public interface ISaveFileInfoTask
{
    Task SaveInfoAsync(Guid fileCode, string userCode, CancellationToken cancellationToken);
}