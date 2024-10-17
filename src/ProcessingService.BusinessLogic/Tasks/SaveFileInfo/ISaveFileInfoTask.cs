namespace ProcessingService.BusinessLogic.Tasks.SaveFileInfo;

public interface ISaveFileInfoTask
{
    Task SaveAsync(string fileCode, string userCode, string fileName, CancellationToken cancellationToken);
}