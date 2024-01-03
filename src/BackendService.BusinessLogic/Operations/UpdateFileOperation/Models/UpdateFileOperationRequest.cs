namespace BackendService.BusinessLogic.Operations.UpdateFileOperation.Models;

public sealed class UpdateFileOperationRequest
{
    public UpdateFileOperationRequest(Guid fileCode, Stream stream, string userCode)
    {
        FileCode = fileCode;
        Stream = stream;
        UserCode = userCode;
    }

    public Guid FileCode { get; set; } = default!;

    public Stream Stream { get; set; } = default!;

    public string UserCode { get; set; } = default!;
}