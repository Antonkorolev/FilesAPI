namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

public sealed class UploadFileOperationRequest
{
    public UploadFileOperationRequest(Stream stream, string fileName, string userCode)
    {
        Stream = stream;
        FileName = fileName;
        UserCode = userCode;
    }

    public Stream Stream { get; set; }

    public string FileName { get; set; }

    public string UserCode { get; set; }
}