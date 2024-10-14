namespace BackendService.BusinessLogic.Operations.UpdateFiles.Models;

public sealed class UpdateFilesOperationRequest(IEnumerable<UpdateFileData> updateFileData, string userCode)
{
    public IEnumerable<UpdateFileData> UpdateFileData { get; set; } = updateFileData;

    public string UserCode { get; set; } = userCode;
}