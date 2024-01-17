using System.IO.Abstractions;
using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.Tasks.DeleteFileTask;

public sealed class DeleteFileTask : IDeleteFileTask
{
    private readonly IFileSystem _fileSystem;

    public DeleteFileTask(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public void Delete(string path)
    {
        if (!_fileSystem.File.Exists(path)) throw new FileNotFoundException(path);
        _fileSystem.File.Delete(path);
    }
}