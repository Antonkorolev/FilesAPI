namespace BackendService.BusinessLogic.Operations.GetFileOperation.Models;

public sealed class GetFileOperationRequest
{
    public GetFileOperationRequest(Guid fileCode, string userCode)
    {
        FileCode = fileCode;
        UserCode = userCode;
    }

    public Guid FileCode { get; set; }

    public string UserCode { get; set; }
}