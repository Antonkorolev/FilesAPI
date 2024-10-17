namespace ProcessingService.BusinessLogic.Tasks.DeleteFile;

public interface IDeleteFileTask
{
    Task DeleteAsync(string path);
}