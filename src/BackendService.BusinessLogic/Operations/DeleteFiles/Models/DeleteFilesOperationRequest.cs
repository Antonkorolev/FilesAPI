namespace BackendService.BusinessLogic.Operations.DeleteFiles.Models;

public sealed class DeleteFilesOperationRequest(IEnumerable<string> fileCodes, string userCode)
{
    public IEnumerable<string> FileCodes { get; set; } = fileCodes;

    public string UserCode { get; set; } = userCode;
}