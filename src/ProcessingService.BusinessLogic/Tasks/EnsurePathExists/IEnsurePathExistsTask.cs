namespace ProcessingService.BusinessLogic.Tasks.EnsurePathExists;

public interface IEnsurePathExistsTask
{
    Task EnsureExistingAsync(string path);
}