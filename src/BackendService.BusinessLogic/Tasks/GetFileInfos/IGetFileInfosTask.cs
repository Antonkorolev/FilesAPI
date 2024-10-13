using BackendService.BusinessLogic.Tasks.GetFileInfos.Models;

namespace BackendService.BusinessLogic.Tasks.GetFileInfos;

public interface IGetFileInfosTask
{
    Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<string> fileCodes);
}