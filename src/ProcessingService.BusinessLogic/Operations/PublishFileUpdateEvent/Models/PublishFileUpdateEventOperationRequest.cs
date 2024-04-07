namespace ProcessingService.BusinessLogic.Operations.PublishFileUpdateEvent.Models;

public sealed class PublishFileUpdateEventOperationRequest
{
    public PublishFileUpdateEventOperationRequest(int updateFileType, string[] filesNames)
    {
        UpdateFileType = updateFileType;
        FilesNames = filesNames;
    }
    
    public int UpdateFileType { get; set; }
    
    public string[] FilesNames { get; set; }
}