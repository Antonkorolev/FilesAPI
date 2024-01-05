namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

public sealed class GetFilesOperationRequest
{
    public GetFilesOperationRequest(IEnumerable<string> fileCodes, string userCode)
    {
        FileCodes = fileCodes;
        UserCode = userCode;
    }

    public IEnumerable<string> FileCodes { get; set; }

    public string UserCode { get; set; }
}