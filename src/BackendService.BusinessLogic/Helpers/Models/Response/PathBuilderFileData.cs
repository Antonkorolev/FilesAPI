namespace BackendService.BusinessLogic.Helpers.Models.Response;

public sealed class PathBuilderFileData
{
    public PathBuilderFileData(string fileName, string path)
    {
        FileName = fileName;
        Path = path;
    }

    public string FileName { get; set; }

    public string Path { get; set; }
}