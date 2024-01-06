namespace BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Request;

public sealed class PathsPreparationTaskRequest
{
    public PathsPreparationTaskRequest(IEnumerable<PathsPreparationTaskFileInfo> fileInfos)
    {
        FileInfos = fileInfos;
    }

    public IEnumerable<PathsPreparationTaskFileInfo> FileInfos { get; set; }
}