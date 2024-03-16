using BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles.Tasks.GetFileInfos;

public interface IGetFileInfosTask
{
    Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<string> fileCodes);
}