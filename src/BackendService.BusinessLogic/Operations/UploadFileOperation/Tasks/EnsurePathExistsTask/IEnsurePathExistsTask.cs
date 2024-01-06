namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.EnsurePathExistsTask;

public interface IEnsurePathExistsTask
{
    void EnsureExisting(string path);
}