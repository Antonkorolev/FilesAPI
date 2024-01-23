using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask;

public interface IGetFilesTask
{
    byte[] Get(GetFilesTaskRequest request);
}