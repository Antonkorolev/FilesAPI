using BackendService.BusinessLogic.Operations.GetFiles.Models;

namespace BackendService.BusinessLogic.Operations.GetFiles;

public interface IGetFilesOperation
{
    Task<byte[]> GetFilesAsync(GetFilesOperationRequest request);
}