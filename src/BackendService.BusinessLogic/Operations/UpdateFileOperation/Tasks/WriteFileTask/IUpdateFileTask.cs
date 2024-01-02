namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Tasks.WriteFileTask;

public interface IUpdateFileTask
{
    Task UpdateAsync(Stream stream, string path);
}