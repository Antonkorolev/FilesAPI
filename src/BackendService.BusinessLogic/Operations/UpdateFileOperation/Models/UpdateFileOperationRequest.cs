namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;

public sealed class UpdateFileOperationRequest
{
    public UpdateFileOperationRequest(Guid fileCode, Stream fileStream, string userCode)
    {
        FileCode = fileCode;
        FileStream = fileStream;
        UserCode = userCode;
    }

    public Guid FileCode { get; set; } = default!;

    public Stream FileStream { get; set; } = default!;

    public string UserCode { get; set; } = default!;
}