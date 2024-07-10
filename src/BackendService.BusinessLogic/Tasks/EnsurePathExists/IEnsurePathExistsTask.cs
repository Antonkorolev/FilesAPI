namespace BackendService.BusinessLogic.Tasks.EnsurePathExists;

public interface IEnsurePathExistsTask
{
    void EnsureExisting(string path);
}