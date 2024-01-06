namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public sealed class GetFileTask : IGetFileTask
{
    public async Task<Stream> GetAsync(string path)
    {
        await using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);

        memoryStream.Position = 0;

        return memoryStream;
    }
}