namespace BackendService.BusinessLogic.Operations.UpdateFile.Tasks.UpdateFileTask;

public sealed class UpdateFileTask : IUpdateFileTask
{
    public async Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = File.Open(path, FileMode.Create);
        
        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}