namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

public sealed class GetFilesOperationRequest
{
    public GetFilesOperationRequest(string[] fileCodes, string userCode)
    {
        FileCodes = fileCodes;
        UserCode = userCode;
    }

    public string[] FileCodes { get; set; }

    public string UserCode { get; set; }
}