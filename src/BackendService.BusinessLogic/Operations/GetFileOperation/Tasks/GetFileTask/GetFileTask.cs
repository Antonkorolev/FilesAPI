using System.IO.Abstractions;

namespace BackendService.BusinessLogic.Operations.GetFileOperation.Tasks.GetFileTask;

public sealed class GetFileTask : IGetFileTask
{
    private readonly IFileSystem _fileSystem;

    public GetFileTask(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public async Task<Stream> GetAsync(string path)
    {
        await using var fileStream = _fileSystem.File.Open(path, FileMode.Open, FileAccess.Read);
        var memoryStream = new MemoryStream();
        await fileStream.CopyToAsync(memoryStream).ConfigureAwait(false);

        memoryStream.Position = 0;

        return memoryStream;
    }
}