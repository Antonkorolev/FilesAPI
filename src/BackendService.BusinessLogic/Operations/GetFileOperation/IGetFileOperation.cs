using BackendService.BusinessLogic.Operations.GetFileOperation.Models;

namespace BackendService.BusinessLogic.Operations.GetFileOperation;

public interface IGetFileOperation
{
    Task<Stream> GetFile(GetFileOperationRequest operationRequest);
}