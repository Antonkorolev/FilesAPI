using Common;

namespace BackendService.BusinessLogic.Tasks.SendUpdateFilesCommand.Models;

public sealed class SendUpdateFilesCommandTaskRequest
{
    public SendUpdateFilesCommandTaskRequest(UpdateFileType updateFileType, IEnumerable<string> filesNames)
    {
        UpdateFileType = updateFileType;
        FilesNames = filesNames;
    }

    public UpdateFileType UpdateFileType { get; }

    public IEnumerable<string> FilesNames { get; }
}