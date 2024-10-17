namespace ProcessingService.BusinessLogic.Operations.UpdateFiles.Tasks.UpdateFile;

public interface IUpdateFileTask
{
    Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken);
}