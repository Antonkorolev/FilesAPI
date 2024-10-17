namespace ProcessingService.BusinessLogic.Operations.UpdateFiles.Models;

public sealed class UpdateFiles(string fileName, string fileCode)
{
    public string FileName { get; set; } = fileName;

    public string FileCode { get; set; } = fileCode;
}