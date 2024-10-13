using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;

public interface IGetFilesTask
{
    Task<byte[]> GetAsync(GetFilesTaskRequest request);
}