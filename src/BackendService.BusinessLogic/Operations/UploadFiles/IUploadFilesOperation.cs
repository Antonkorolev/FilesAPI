using BackendService.BusinessLogic.Operations.UploadFiles.Models;

namespace BackendService.BusinessLogic.Operations.UploadFiles;

public interface IUploadFilesOperation
{
    Task<IEnumerable<string>> UploadAsync(UploadFilesOperationRequest request);
}