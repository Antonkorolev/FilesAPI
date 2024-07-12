using Common;

namespace BackendService.BusinessLogic.Tasks.SendNotificationCommand.Models;

public sealed class SendNotificationCommandTaskRequest
{
    public SendNotificationCommandTaskRequest(UpdateFileType updateFileType, IEnumerable<string> filesNames)
    {
        UpdateFileType = updateFileType;
        FilesNames = filesNames;
    }

    public UpdateFileType UpdateFileType { get; }

    public IEnumerable<string> FilesNames { get; }
}