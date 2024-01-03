namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.SaveFileInfoTask;

public interface ISaveFileInfoTask
{
    Task SaveAsync(Guid fileCode, string userCode, string fileName, CancellationToken cancellationToken);
}