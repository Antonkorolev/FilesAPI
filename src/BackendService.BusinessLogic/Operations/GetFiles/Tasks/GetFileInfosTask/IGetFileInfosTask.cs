using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfosTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfosTask;

public interface IGetFileInfosTask
{
    Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<string> fileCodes);
}