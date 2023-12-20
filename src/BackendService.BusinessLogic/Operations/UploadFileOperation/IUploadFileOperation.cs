using BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

namespace BackendService.BusinessLogic.Operations.UploadFileOperation;

public interface IUploadFileOperation
{
    Task<string> UploadFileAsync(UploadFileOperationRequest request);
}