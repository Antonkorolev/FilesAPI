namespace BackendService.BusinessLogic.Operations.UpdateFiles.Tasks.SendUpdateFilesCommand.Models;

public sealed class SendUpdateFilesCommandTaskRequest(IList<SendUpdateFilesData> sendUpdateFilesData)
{
    public IList<SendUpdateFilesData> SendUpdateFilesData { get; set; } = sendUpdateFilesData;
}