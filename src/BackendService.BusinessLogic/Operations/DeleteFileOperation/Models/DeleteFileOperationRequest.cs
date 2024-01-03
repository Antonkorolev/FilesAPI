namespace BackendService.BusinessLogic.Operations.DeleteFileOperation.Models;

public sealed class DeleteFileOperationRequest
{
    public DeleteFileOperationRequest(Guid fileCode, string userCode)
    {
        FileCode = fileCode;
        UserCode = userCode;
    }

    public Guid FileCode { get; set; }

    public string UserCode { get; set; }
}