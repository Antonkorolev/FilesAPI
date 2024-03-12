namespace BackendService.BusinessLogic.Operations.GetFiles.Models;

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