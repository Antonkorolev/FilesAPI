namespace ProcessingService.BusinessLogic.Operations.UploadFiles.Models;

public sealed class UploadFiles
{
    public UploadFiles(string fileName, string fileCode)
    {
        FileName = fileName;
        FileCode = fileCode;
    }

    public string FileName { get; set; }

    public string FileCode { get; set; }
}