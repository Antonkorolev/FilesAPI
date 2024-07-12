namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.WriteFileToPersistenceStorage;

public sealed class WriteFileToPersistenceStorage : IWriteFileToPersistenceStorage
{
    public async Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = File.Create(path);

        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}