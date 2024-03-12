namespace BackendService.BusinessLogic.Operations.DeleteFile.Models;

public sealed class DeleteFileOperationRequest
{
    public DeleteFileOperationRequest(string fileCode, string userCode)
    {
        FileCode = fileCode;
        UserCode = userCode;
    }

    public string FileCode { get; set; }

    public string UserCode { get; set; }
}