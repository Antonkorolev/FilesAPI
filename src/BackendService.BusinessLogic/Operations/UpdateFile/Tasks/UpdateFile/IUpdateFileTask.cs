namespace BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFile;

public interface IUpdateFileTask
{
    Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken);
}