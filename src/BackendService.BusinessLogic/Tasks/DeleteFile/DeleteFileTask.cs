using FileNotFoundException = BackendService.BusinessLogic.Exceptions.FileNotFoundException;

namespace BackendService.BusinessLogic.Tasks.DeleteFile;

public sealed class DeleteFileTask : IDeleteFileTask
{
    public Task DeleteAsync(string path)
    {
        try
        {
            if (!File.Exists(path)) throw new FileNotFoundException(path);
            File.Delete(path);

            return Task.CompletedTask;
        }
        catch(Exception e)
        {
            return Task.FromException(e);
        }
       
    }
}