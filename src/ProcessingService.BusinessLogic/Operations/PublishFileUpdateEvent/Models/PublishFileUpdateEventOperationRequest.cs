namespace ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

public sealed class PublishFileUpdateEventOperationRequest
{
    public PublishFileUpdateEventOperationRequest(int updateFileType, IEnumerable<string> filesNames)
    {
        UpdateFileType = updateFileType;
        FilesNames = filesNames;
    }

    public int UpdateFileType { get; set; }

    public IEnumerable<string> FilesNames { get; set; }
}