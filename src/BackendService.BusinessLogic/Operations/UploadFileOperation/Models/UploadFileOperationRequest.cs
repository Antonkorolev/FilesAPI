namespace BackendService.BusinessLogic.Operations.UploadFileOperation.Models;

public sealed class UploadFileOperationRequest
{
    public UploadFileOperationRequest(Guid fileCode, Stream stream, string userCode)
    {
        FileCode = fileCode;
        Stream = stream;
        UserCode = userCode;
    }

    public Guid FileCode { get; set; }

    public Stream Stream { get; set; } = default!;

    public string UserCode { get; set; } = default!;
}