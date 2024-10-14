namespace BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.Models;

public sealed class SendUpdateFilesData(string fileName, string fileCode)
{
    public string FileName { get; set; } = fileName;

    public string FileCode { get; set; } = fileCode;
}