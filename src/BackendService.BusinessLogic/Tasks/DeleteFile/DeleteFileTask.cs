using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.Tasks.DeleteFile;

public sealed class DeleteFileTask : IDeleteFileTask
{
    public void Delete(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException(path);
        File.Delete(path);
    }
}