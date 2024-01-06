namespace BackendService.BusinessLogic.Tasks.PathsPreparationTask.Models.Response;

public sealed class PathsPreparationTaskFileData
{
    public PathsPreparationTaskFileData(string fileName, string path)
    {
        FileName = fileName;
        Path = path;
    }

    public string FileName { get; set; }

    public string Path { get; set; }
}