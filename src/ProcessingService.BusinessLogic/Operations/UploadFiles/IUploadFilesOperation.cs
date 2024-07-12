using ProcessingService.BusinessLogic.Operations.UploadFiles.Models;

namespace ProcessingService.BusinessLogic.Operations.UploadFiles;

public interface IUploadFilesOperation
{
    Task UploadFilesAsync(UploadFilesOperationRequest request);
}