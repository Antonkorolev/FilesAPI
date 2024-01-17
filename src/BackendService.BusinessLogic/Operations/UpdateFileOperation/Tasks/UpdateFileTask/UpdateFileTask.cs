using System.IO.Abstractions;

namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.UpdateFileTask;

public sealed class UpdateFileTask : IUpdateFileTask
{
    private readonly IFileSystem _fileSystem;

    public UpdateFileTask(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }


    public async Task UpdateAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = _fileSystem.File.Open(path, FileMode.Create);

        stream.Position = 0;
        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}