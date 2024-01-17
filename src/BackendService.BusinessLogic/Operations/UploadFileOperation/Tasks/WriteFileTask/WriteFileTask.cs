using System.IO.Abstractions;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.WriteFileTask;

public sealed class WriteFileTask : IWriteFileTask
{
    private readonly IFileSystem _fileSystem;

    public WriteFileTask(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }


    public async Task WriteAsync(Stream stream, string path, CancellationToken cancellationToken)
    {
        await using var streamToWrite = _fileSystem.File.Create(path);

        await stream.CopyToAsync(streamToWrite, cancellationToken).ConfigureAwait(false);
        await stream.DisposeAsync();
    }
}