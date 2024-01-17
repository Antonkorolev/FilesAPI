using System.IO.Abstractions;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Tasks.EnsurePathExistsTask;

public sealed class EnsurePathExistsTask : IEnsurePathExistsTask
{
    private readonly IFileSystem _fileSystem;

    public EnsurePathExistsTask(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void EnsureExisting(string path)
    {
        var directory = _fileSystem.Path.GetDirectoryName(path);

        if (string.IsNullOrEmpty(directory))
            throw new DirectoryNotFoundException($"Can't get directory from path = '{path}'");

        if (!_fileSystem.Directory.Exists(directory))
            _fileSystem.Directory.CreateDirectory(directory);
    }
}