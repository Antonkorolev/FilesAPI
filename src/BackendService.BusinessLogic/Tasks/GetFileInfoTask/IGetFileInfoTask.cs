using BackendService.BusinessLogic.Tasks.GetFileInfoTask.Models;

namespace BackendService.BusinessLogic.Tasks.GetFileInfoTask;

public interface IGetFileInfoTask
{
    Task<GetFileInfoTaskResponse> GetAsync(Guid fileCode);
}