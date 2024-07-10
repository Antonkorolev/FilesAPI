using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UploadFIles;

public sealed class UploadFilesRequest
{
    public IList<IFormFile> Files { get; set; } = default!;
}