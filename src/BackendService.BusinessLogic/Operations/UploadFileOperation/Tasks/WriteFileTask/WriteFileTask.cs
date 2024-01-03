namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;

public sealed class WriteFileTask : IWriteFileTask
{
    public async Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = File.Open(path, FileMode.CreateNew);

        stream.Position = 0;
        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}