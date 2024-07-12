namespace BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;

public sealed class SendUploadFilesData
{
    public SendUploadFilesData(string fileName, string fileCode)
    {
        FileName = fileName;
        FileCode = fileCode;
    }

    public string FileName { get; set; }

    public string FileCode { get; set; }
}