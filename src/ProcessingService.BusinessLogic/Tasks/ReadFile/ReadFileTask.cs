namespace ProcessingService.BusinessLogic.Tasks.ReadFile;

public sealed class ReadFileTask : IReadFileTask
{
    public async Task<Stream> ReadFileAsync(string path)
    {
        await using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read);

        var memoryStream = new MemoryStream();

        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}