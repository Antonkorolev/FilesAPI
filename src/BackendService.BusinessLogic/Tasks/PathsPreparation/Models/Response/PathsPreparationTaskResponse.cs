namespace BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Response;

public sealed class PathsPreparationTaskResponse
{
    public PathsPreparationTaskResponse(IEnumerable<PathsPreparationTaskFileData> fileData)
    {
        FileData = fileData;
    }

    public IEnumerable<PathsPreparationTaskFileData> FileData { get; set; }
}