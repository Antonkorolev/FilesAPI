namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public sealed class GetFileTask : IGetFileTask
{
    public async Task<Stream> GetAsync(string path)
    {
        await using var fileStream = File.Open(path, FileMode.Open, FileAccess.Read);

        var memoryStream = new MemoryStream();

        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}