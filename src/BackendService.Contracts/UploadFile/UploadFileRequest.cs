using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UploadFile;

public sealed class UploadFileRequest
{
    public IFormFile File { get; set; } = default!;
}