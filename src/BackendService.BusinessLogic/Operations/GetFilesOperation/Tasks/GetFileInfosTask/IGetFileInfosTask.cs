using BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFileInfosTask;

public interface IGetFileInfosTask
{
    Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<Guid> fileCodes);
}