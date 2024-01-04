using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UpdateFile;

public sealed class UpdateFileRequest
{
    public Guid FileCode { get; set; } = default!;

    public IFormFile File { get; set; } = default!;
}
