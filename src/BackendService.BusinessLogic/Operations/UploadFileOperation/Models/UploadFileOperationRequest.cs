namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

public sealed class UploadFileOperationRequest
{
    public UploadFileOperationRequest(string fileCode, Stream stream, string userCode)
    {
        FileCode = fileCode;
        Stream = stream;
        UserCode = userCode;
    }

    public string FileCode { get; set; } = default!;

    public Stream Stream { get; set; } = default!;

    public string UserCode { get; set; } = default!;
}