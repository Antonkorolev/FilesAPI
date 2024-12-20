namespace BackendService.BusinessLogic.Operations.GetFile.Models;

public sealed class GetFileOperationRequest
{
    public GetFileOperationRequest(string fileCode, string userCode)
    {
        FileCode = fileCode;
        UserCode = userCode;
    }

    public string FileCode { get; set; }

    public string UserCode { get; set; }
}