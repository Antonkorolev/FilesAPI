using Microsoft.AspNetCore.Http;

namespace BackendService.Contracts.UpdateFiles;

public sealed class UpdateFile
{
    public string FileCode { get; set; } = default!;

    public string FileName { get; set; } = default!;

    public IFormFile File { get; set; } = default!;
}