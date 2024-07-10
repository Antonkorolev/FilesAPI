namespace BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;

public sealed class SendUploadFilesData
{
    public SendUploadFilesData(string fileName, string filePath)
    {
        FileName = fileName;
        FilePath = filePath;
    }

    public string FileName { get; set; }

    public string FilePath { get; set; }
}