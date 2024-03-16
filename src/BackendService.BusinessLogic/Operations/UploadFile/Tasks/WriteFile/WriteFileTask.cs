namespace BackendService.BusinessLogic.Operations.UploadFile.Tasks.WriteFile;

public sealed class WriteFileTask : IWriteFileTask
{
    public async Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = File.Create(path);

        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}