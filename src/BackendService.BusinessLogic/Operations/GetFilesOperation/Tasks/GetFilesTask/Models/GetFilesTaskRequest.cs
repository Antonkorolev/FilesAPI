namespace BackendService.BusinessLogic.Operations.GetFilesOperation.Tasks.GetFilesTask.Models;

public sealed class GetFilesTaskRequest
{
    public GetFilesTaskRequest(IEnumerable<GetFilesTaskFileData> fileData)
    {
        FileData = fileData;
    }

    public IEnumerable<GetFilesTaskFileData> FileData { get; set; }
}