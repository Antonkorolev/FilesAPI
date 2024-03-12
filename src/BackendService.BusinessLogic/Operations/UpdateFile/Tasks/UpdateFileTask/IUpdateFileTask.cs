namespace BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileTask;

public interface IUpdateFileTask
{
    Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken);
}