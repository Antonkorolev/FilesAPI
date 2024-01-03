namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Models;

public sealed class GetFilesOperationRequest
{
    public GetFilesOperationRequest(IEnumerable<Guid> fileCodes, string userCode)
    {
        FileCodes = fileCodes;
        UserCode = userCode;
    }

    public IEnumerable<Guid> FileCodes { get; set; }

    public string UserCode { get; set; }
}