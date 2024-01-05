using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UpdateFile;

public sealed class UpdateFileRequest
{
    public string FileCode { get; set; } = default!;

    public IFormFile File { get; set; } = default!;
}
