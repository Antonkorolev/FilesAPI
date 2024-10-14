namespace BackendService.BusinessLogic.Operations.UpdateFiles.Models;

public sealed class UpdateFileData(Stream stream, string fileName, string fileCode)
{
    public Stream Stream { get; set; } = stream;

    public string FileName { get; set; } = fileName;

    public string FileCode { get; set; } = fileCode;
}