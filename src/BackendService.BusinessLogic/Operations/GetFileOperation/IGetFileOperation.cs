using BackendService.BusinessLogic.Operations.GetFileOperation.Models;

namespace BackendService.BusinessLogic.Operations.GetFileOperation;

public interface IGetFileOperation
{
    Task<GetFileOperationResponse> GetFileAsync(GetFileOperationRequest operationRequest);
}