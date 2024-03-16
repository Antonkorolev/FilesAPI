namespace BackendService.BusinessLogic.Operations.GetFile.Tasks.GetFile;

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