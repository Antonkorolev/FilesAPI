namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Tasks.ReadFilesFromTemporaryStorage;

public sealed class ReadFilesFromTemporaryStorageTask : IReadFilesFromTemporaryStorageTask
{
    public async Task<Stream> RadFileAsync(string path)
    {
        await using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read);

        var memoryStream = new MemoryStream();

        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}