namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

public sealed class UploadFileOperationRequest
{
    public UploadFileOperationRequest(string fileCode, Stream fileStream, string userCode)
    {
        FileCode = fileCode;
        FileStream = fileStream;
        UserCode = userCode;
    }

    public string FileCode { get; set; } = default!;

    public Stream FileStream { get; set; } = default!;

    public string UserCode { get; set; } = default!;
}