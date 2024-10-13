namespace BackendService.BusinessLogic.Tasks.EnsurePathExists;

public interface IEnsurePathExistsTask
{
    Task EnsureExistingAsync(string path);
}