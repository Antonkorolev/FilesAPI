using BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

namespace BackendService.BusinessLogic.Operations.GetFilesOperation;

public interface IGetFilesOperation
{
    Task<byte[]> GetFilesAsync(GetFilesOperationRequest request);
}