using ProcessingService.BusinessLogic.Tasks.GetFileInfos.Models;

namespace ProcessingService.BusinessLogic.Tasks.GetFileInfos;

public interface IGetFileInfosTask
{
    Task<GetFileInfosTaskResponse> GetAsync(IEnumerable<string> fileCodes);
}