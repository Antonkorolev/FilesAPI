namespace BackendService.BusinessLogic.Operations.UploadFiles.Models;

public sealed class UploadFilesOperationRequest
{
    public UploadFilesOperationRequest(IEnumerable<UploadFileData> uploadFileData, string userCode)
    {
        UploadFileData = uploadFileData;
        UserCode = userCode;
    }

    public IEnumerable<UploadFileData> UploadFileData { get; set; }
    
    public string UserCode { get; set; }
}