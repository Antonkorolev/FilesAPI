using BackendService.BusinessLogic.Operations.GetFile.Models;

namespace BackendService.BusinessLogic.Operations.GetFile;

public interface IGetFileOperation
{
    Task<GetFileOperationResponse> GetFileAsync(GetFileOperationRequest operationRequest);
}