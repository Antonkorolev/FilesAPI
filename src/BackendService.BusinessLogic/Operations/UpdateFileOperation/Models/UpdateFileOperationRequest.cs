namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;

public sealed class UpdateFileOperationRequest
{
    public UpdateFileOperationRequest(string fileCode, Stream stream, string fileName, string userCode)
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