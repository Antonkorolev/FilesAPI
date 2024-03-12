using BackendService.BusinessLogic.Operations.UploadFile.Models;

namespace BackendService.BusinessLogic.Operations.UploadFile;

public interface IUploadFileOperation
{
    Task<string> UploadAsync(UploadFileOperationRequest request);
}