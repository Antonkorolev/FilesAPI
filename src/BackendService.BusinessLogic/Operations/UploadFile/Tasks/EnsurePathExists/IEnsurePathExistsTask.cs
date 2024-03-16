namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.EnsurePathExists;

public interface IEnsurePathExistsTask
{
    void EnsureExisting(string path);
}