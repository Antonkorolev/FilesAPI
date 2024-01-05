namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

public sealed class UploadFileOperationRequest
{
    public UploadFileOperationRequest(string fileCode, Stream stream, string fileName, string userCode)
    {
        FileCode = fileCode;
        Stream = stream;
        FileName = fileName;
        UserCode = userCode;
    }

    public string FileCode { get; set; }

    public Stream Stream { get; set; }
    
    public string FileName { get; set; }

    public string UserCode { get; set; }
}