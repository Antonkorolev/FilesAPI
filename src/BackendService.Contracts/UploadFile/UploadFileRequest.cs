using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UploadFile;

public sealed class UploadFileRequest
{
    public Guid FileCode { get; set; } = default!;

    public IFormFile File { get; set; } = default!;
}
