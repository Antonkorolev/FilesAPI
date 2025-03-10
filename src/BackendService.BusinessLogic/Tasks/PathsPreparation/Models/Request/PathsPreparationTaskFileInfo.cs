namespace BackendService.BusinessLogic.Tasks.PathsPreparation.Models.Request;

public sealed class PathsPreparationTaskFileInfo
{
    public PathsPreparationTaskFileInfo(string fileCode, string fileName)
    {
        FileCode = fileCode;
        FileName = fileName;
    }

    public string FileCode { get; set; }

    public string FileName { get; set; }
}