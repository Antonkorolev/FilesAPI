namespace BackendService.BusinessLogic.Operations.UploadFiles.Tasks.SendUploadFilesCommand.Models;

public sealed class SendUploadFilesCommandTaskRequest
{
    public SendUploadFilesCommandTaskRequest(IList<SendUploadFilesData> sendUploadFilesData)
    {
        SendUploadFilesData = sendUploadFilesData;
    }

    public IList<SendUploadFilesData> SendUploadFilesData { get; set; }
}