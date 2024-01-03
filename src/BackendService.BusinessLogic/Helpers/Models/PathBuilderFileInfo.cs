namespace BackendService.BusinessLogic.Helpers.Models;

public sealed class PathBuilderFileInfo
{
    public PathBuilderFileInfo(string fileCode, string fileName)
    {
        FileCode = fileCode;
        FileName = fileName;
    }

    public string FileCode { get; set; }

    public string FileName { get; set; }
}