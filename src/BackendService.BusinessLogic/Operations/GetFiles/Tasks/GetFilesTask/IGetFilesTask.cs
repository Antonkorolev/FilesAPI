using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask;

public interface IGetFilesTask
{
    byte[] Get(GetFilesTaskRequest request);
}