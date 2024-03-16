using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFilesTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFiles;

public interface IGetFilesTask
{
    byte[] Get(GetFilesTaskRequest request);
}