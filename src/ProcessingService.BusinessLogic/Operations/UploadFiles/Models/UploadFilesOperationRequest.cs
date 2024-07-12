namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Models;

public sealed class UploadFilesOperationRequest
{
    public UploadFilesOperationRequest(IEnumerable<UploadFiles> uploadFiles)
    {
        UploadFiles = uploadFiles;
    }

    public IEnumerable<UploadFiles> UploadFiles { get; set; }
}