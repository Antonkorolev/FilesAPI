using BackendService.BusinessLogic.Tasks.GetFileInfo.Models;

namespace BackendService.BusinessLogic.Tasks.GetFileInfo;

public interface IGetFileInfoTask
{
    Task<GetFileInfoTaskResponse> GetAsync(string fileCode);
}