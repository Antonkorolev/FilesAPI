namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExistsTask;

public interface IEnsurePathExistsTask
{
    void EnsureExisting(string path);
}